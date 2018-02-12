using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grumpydev.Net.Essentials.Web;
using Microsoft.AspNetCore.Server.Kestrel;


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