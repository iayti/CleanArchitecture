using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using CleanArchitecture.Api;

namespace CleanArchitecture.Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;
        private static string _currentUserId;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTestsAsync()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "CleanArchitecture.Api"));

            services.AddLogging();

            startup.ConfigureServices(services);

            // Replace service registration for ICurrentUserService
            // Remove existing registration
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            // Register testing version
            services.AddTransient(_ =>
                Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
            };

            await ResetSqliteDbAsync();
            await EnsureDatabaseAsync();
        }

        private static async Task EnsureDatabaseAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (context != null)
            {
                await context.Database.MigrateAsync();
            }
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator!.Send(request);
        }

        public static async Task<string> RunAsDefaultUserAsync()
        {
            var rand = new Random();
            return await RunAsUserAsync($"test.{rand.Next()}@local", "Testing1234!", new string[] { });
        }

        public static async Task<string> RunAsAdministratorAsync()
        {
            return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { "Administrator" });
        }

        private static async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<CleanArchitecture.Infrastructure.Identity.ApplicationUser>>();

            var user = new CleanArchitecture.Infrastructure.Identity.ApplicationUser { UserName = userName, Email = userName };

            var result = await userManager!.CreateAsync(user, password);

            if (roles.Any())
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                foreach (var role in roles)
                {
                    await roleManager!.CreateAsync(new IdentityRole(role));
                }

                await userManager.AddToRolesAsync(user, roles);
            }

            if (result.Succeeded)
            {
                _currentUserId = user.Id;

                return _currentUserId;
            }

            var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

            throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
        }

        public static async Task ResetState()
        {
            var provider = _configuration.GetValue("DbProvider", "SqlServer");

            if (provider.Equals("Sqlite"))
            {
                // If with Sqlite, the CheckPoint does not support Sqlite yet.
                // It may need special treatment, and cannot set to in-memory
                
                // remove sqlite db
                await ResetSqliteDbAsync();

                await EnsureDatabaseAsync();
            }
            else
            {
                await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
                _currentUserId = null;
            }
        }
        
        private static async Task ResetSqliteDbAsync()
        {
            var provider = _configuration.GetValue("DbProvider", "SqlServer");
            if (!provider.Equals("Sqlite"))
            {
                return;
            }
            
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
            }
        }

        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context!.FindAsync<TEntity>(keyValues);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (context != null)
            {
                context.Add(entity);

                await context.SaveChangesAsync();
            }
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // everthing for testing should be torn down here
        }
    }
}
