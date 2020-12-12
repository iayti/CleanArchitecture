using System;
using Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.Common.Exceptions
{
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
