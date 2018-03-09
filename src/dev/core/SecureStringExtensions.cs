using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Grumpydev.Net.Essentials.Core
{
    public static class SecureStringExtensions
    {
        public static string ConvertToUnsecureString(this SecureString secureString)
        {
            secureString.ThrowIfNull();

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static SecureString ConvertToSecureString(this string unsecureString)
        {
            unsecureString.ThrowIfNull();

            unsafe
            {
                fixed (char* passwordChars = unsecureString)
                {
                    var securePassword = new SecureString(passwordChars, unsecureString.Length);
                    securePassword.MakeReadOnly();
                    return securePassword;
                }
            }
        }
    }
}
