using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.UnitTests.ValueObjects
{
    public class AdAccountTests
    {
        [Test]
        public void ShouldHaveCorrectDomainAndName()
        {
            const string accountString = "ilker\\Ayti";

            var account = AdAccount.For(accountString);

            account.Domain.Should().Be("ilker");
            account.Name.Should().Be("Ayti");
        }

        [Test]
        public void ToStringReturnsCorrectFormat()
        {
            const string accountString = "ilker\\Ayti";

            var account = AdAccount.For(accountString);

            var result = account.ToString();

            result.Should().Be(accountString);
        }

        [Test]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            const string accountString = "ilker\\Ayti";

            var account = AdAccount.For(accountString);

            string result = account;

            result.Should().Be(accountString);
        }

        [Test]
        public void ExplicitConversionFromStringSetsDomainAndName()
        {
            const string accountString = "ilker\\Ayti";

            var account = (AdAccount)accountString;

            account.Domain.Should().Be("ilker");
            account.Name.Should().Be("Ayti");
        }

        [Test]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidAdAccount()
        {
            FluentActions.Invoking(() => (AdAccount)"ilkerAyti")
                .Should().Throw<AdAccountInvalidException>();
        }
    }
}
