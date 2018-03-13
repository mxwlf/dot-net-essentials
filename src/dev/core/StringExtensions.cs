using System;
using System.Collections.Generic;
using System.Text;

namespace Grumpydev.Net.Essentials.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// Combines Null Empty and Whitespace checks.
        /// </summary>
        /// <param name="value">
        /// The string to be checked.
        /// </param>
        /// <returns>
        /// True if the value is null, empty or consists of only whitespace characters. Otherwise, false.
        /// </returns>
        public static bool IsNullEmptyOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
    }
}
