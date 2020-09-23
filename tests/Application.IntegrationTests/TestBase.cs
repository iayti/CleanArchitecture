namespace Application.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;
    using static Testing;

    public class TestBase : IAsyncLifetime//, IClassFixture<Testing>
    {
        public async Task InitializeAsync()
        {
            //await Task.Run(ResetState);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

    }
}
