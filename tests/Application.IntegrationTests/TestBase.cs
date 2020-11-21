using NUnit.Framework;
using System.Threading.Tasks;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests
{
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
