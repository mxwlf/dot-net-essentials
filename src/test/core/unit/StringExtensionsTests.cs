using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grumpydev.Net.Essentials.Core.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void IsNullEmptyOrWhitespace_GivenStringDotEmpty_ExpectTrue()
        {
            // Arrange
            var emptyString = string.Empty;

            // Act, Assert
            Assert.IsTrue(emptyString.IsNullEmptyOrWhiteSpace());
        }


        [TestMethod]
        public void IsNullEmptyOrWhitespace_GivenNullString_ExpectTrue()
        {
            // Arrange
            string nullString = null;

            // Act, Assert
            Assert.IsTrue(nullString.IsNullEmptyOrWhiteSpace());
        }

        [TestMethod]
        public void IsNullEmptyOrWhitespace_GivenStringOfTabCharacters_ExpectTrue()
        {
            // Arrange
            var tabString = new string('\t', 10);

            // Act, Assert
            Assert.IsTrue(tabString.IsNullEmptyOrWhiteSpace());
        }

        [TestMethod]
        public void IsNullEmptyOrWhitespace_GivenUnicodeWhiteSpace_ExpectTrue()
        {
            // Arrange
            var whiteSpaceString = new string('\u2000', 10);

            // Act, Assert
            Assert.IsTrue(whiteSpaceString.IsNullEmptyOrWhiteSpace());
        }

        [TestMethod]
        public void IsNullEmptyOrWhitespace_GivenOnlySpaces_ExpectTrue()
        {
            // Arrange
            var whiteSpaces = new string(' ', 10);

            // Act, Assert
            Assert.IsTrue(whiteSpaces.IsNullEmptyOrWhiteSpace());
        }


        [TestMethod]
        public void IsNullEmptyOrWhitespace_GivenNonEmptyString_ExpectFalse()
        {
            // Arrange
            const string NonEmptyString = "ABCDE";

            // Act, Assert
            Assert.IsFalse(NonEmptyString.IsNullEmptyOrWhiteSpace());
        }

        [TestMethod]
        public void IsNullEmptyOrWhitespace_GivenNonEmptyStringWithWhiteSpaceChars_ExpectFalse()
        {
            // Arrange
            const string NonEmptyString = "  AB CD EF  ";

            // Act, Assert
            Assert.IsFalse(NonEmptyString.IsNullEmptyOrWhiteSpace());
        }

        [TestMethod]
        public void IsNullEmptyOrWhitespace_GivenSpecialChars_ExpectFalse()
        {
            // Arrange
            // UTF-16 Char 2 is START OF TEXT (U+0002)
            var nonEmptyString = new string((char)2, 1);

            // Act, Assert
            Assert.IsFalse(string.IsNullOrEmpty(nonEmptyString));
        }

    }
}
