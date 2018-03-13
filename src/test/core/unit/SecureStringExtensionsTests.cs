using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.Security;

namespace Grumpydev.Net.Essentials.Core.Tests
{
    [TestClass]
    public class SecureStringExtensionsTests
    {
        [TestMethod]
        public void ConvertToSecureString_WhenUnsecureStringPassed_ShouldReturnASecureString()
        {
            // Arrange.
            var unsecureString = "Usecured String";

            // Act.
            var secureString = unsecureString.ConvertToSecureString();

            // Assert.
            secureString.Should().NotBeNull();
            secureString.GetType().FullName.Should().Be("System.Security.SecureString");
        }

        [TestMethod]
        public void ConvertToUnsecureString_WhenSecureStringPassed_ShouldReturnOriginalString()
        {
            // Arrange.
            var unsecureString = "Usecured String";
            var secureString = unsecureString.ConvertToSecureString();

            // Act.
            var result = secureString.ConvertToUnsecureString();

            // Assert.
            result.Should().NotBeEmpty();
            result.Should().Be("Usecured String");
        }

        [TestMethod]
        public void ConvertToSecureString_WhenPassingEmptyString_ShouldConvertToSecureString()
        {
            // Arrange.
            var unsecureString = string.Empty;

            // Act.
            var secureString = unsecureString.ConvertToSecureString();

            // Assert.
            var result = secureString.ConvertToUnsecureString();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void ConvertToUnsecureString_WhenNull_ShouldThrowArgumentNullException()
        {
            // Arrange.
            SecureString secureString = null;

            Action action = () => { secureString.ConvertToUnsecureString(); };

            // Assert.
            action.Should().Throw<Exception>("secureString");
        }

        [TestMethod]
        public void ConvertToSecureString_WhenNull_ShouldThrowArgumentNullException()
        {
            // Arrange.
            string unsecureString = null;

            Action action = () => { unsecureString.ConvertToSecureString(); };

            // Assert.
            action.Should().Throw<Exception>("unsecureString");
        }
    }
}