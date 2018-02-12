using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

[TestClass]
public class KestrelExtensionsTests
{
   protected static string myField = "";

    [TestMethod]
    public void Test1()
    {
        var options = new KestrelServerOptions();
    }
}