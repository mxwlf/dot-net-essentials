using System;
using System.Collections.Generic;
using System.Text;

namespace Grumpydev.Net.Essentials.Core
{
    public static class CoreExtensions
    {
        public static void ThrowIfNull(this object @object, string message = null)
        {
            if (@object == null)
            {
                throw new Exception(message ?? "Variable expected to be not null");
            }
        }

        public static void ThrowIfNullEmptyOrWhiteSpace(this string @string, string message = null)
        {
            if(@string.IsNullEmptyOrWhiteSpace())
            {
                throw new Exception(message ?? "The string is expected to be non null, nor expty or white space.");
            }
        }
    }
}
