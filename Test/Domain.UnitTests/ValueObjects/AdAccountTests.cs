namespace Domain.UnitTests.ValueObjects
{
    using Domain.ValueObjects;
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class AdAccountTests
    {
        [Fact]
        public void ShouldHaveCorrectDomainAndName()
        {
            const string accountString = "SSW\\Jason";

            var account = AdAccount.For(accountString);

            account.Domain.Should().Be("SSW");
            account.Name.Should().Be("Jason");
        }

        [Fact]
        public void ToStringReturnsCorrectFormat()
        {
            const string accountString = "SSW\\Jason";

            var account = AdAccount.For(accountString);

            var result = account.ToString();

            result.Should().Be(accountString);
        }

        [Fact]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            const string accountString = "SSW\\Jason";

            var account = AdAccount.For(accountString);

            string result = account;

            result.Should().Be(accountString);
        }

        [Fact]
        public void ExplicitConversionFromStringSetsDomainAndName()
        {
            const string accountString = "SSW\\Jason";

            var account = (AdAccount)accountString;

            account.Domain.Should().Be("SSW");
            account.Name.Should().Be("Jason");
        }

        [Fact]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidAdAccount()
        {
            FluentActions.Invoking(() => (AdAccount)"SSWJason")
                .Should().Throw<AdAccountInvalidException>();
        }
    }
}
