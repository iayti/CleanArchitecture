namespace Application.IntegrationTests
{
    using System.Threading.Tasks;
    using NUnit.Framework;
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
