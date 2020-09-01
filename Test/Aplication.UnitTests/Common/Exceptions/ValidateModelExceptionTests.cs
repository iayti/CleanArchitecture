namespace Aplication.UnitTests.Common.Exceptions
{
    using System;

    using FluentAssertions;
    using Xunit;

    using Application.Common.Exceptions;

    public class ValidateModelExceptionTests
    {
        [Fact]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidateModelException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

    }
}
