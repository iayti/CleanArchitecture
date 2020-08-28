namespace Infrastructure
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;

    using Application.Common.Interfaces;
    using Identity;
    using IdentityModel;
    using IdentityServer4.Models;
    using IdentityServer4.Test;

    using Persistence;
    using Services;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)//, IWebHostEnvironment environment)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //if (environment.IsEnvironment("Test"))
            //{
            //    services.AddIdentityServer()
            //        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
            //        {
            //            options.Clients.Add(new Client
            //            {
            //                ClientId = "CleanArchitecture.IntegrationTests",
            //                AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
            //                ClientSecrets = { new Secret("secret".Sha256()) },
            //                AllowedScopes = { "CleanArchitecture.WebApi", "openid", "profile" }
            //            });
            //        }).AddTestUsers(new List<TestUser>
            //        {
            //            new TestUser
            //            {
            //                SubjectId = "414B0977-9C65-42A9-BF29-E3D03786E78E",
            //                Username = "ilker@clean-architecture",
            //                Password = "CleanArchitecture!",
            //                Claims = new List<Claim>
            //                {
            //                    new Claim(JwtClaimTypes.Email,"ilker@clean-architecture")
            //                }
            //            }
            //        });
            //}
            //else
            //{
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            //}

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
