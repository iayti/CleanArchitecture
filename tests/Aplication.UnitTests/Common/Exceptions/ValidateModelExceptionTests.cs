namespace Application.UnitTests.Common.Exceptions
{
    using System;

    using FluentAssertions;

    using Application.Common.Exceptions;
    using NUnit.Framework;

    public class ValidateModelExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidateModelException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

    }
}
