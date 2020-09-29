namespace Application.UnitTests.Common.Behaviours
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Moq;
    using NUnit.Framework;

    using Application.Cities.Commands.Create;
    using Application.Common.Behaviours;
    using Application.Common.Interfaces;

    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<CreateCityCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;


        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<CreateCityCommand>>();

            _currentUserService = new Mock<ICurrentUserService>();

            _identityService = new Mock<IIdentityService>();
        }

        [Test]
        public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        {
            _currentUserService.Setup(x => x.UserId).Returns("Administrator");

            var requestLogger = new LoggingBehaviour<CreateCityCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await requestLogger.Process(new CreateCityCommand { Name = "Bursa" }, new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var requestLogger = new LoggingBehaviour<CreateCityCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await requestLogger.Process(new CreateCityCommand { Name = "Bursa" }, new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        }
    }
}
