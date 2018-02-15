using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Grumpydev.Net.Essentials.Web;
using FluentAssertions;


[TestClass]
public class KestrelServerOptionsExtensionsTest
{
    [TestMethod]
    public void ConfigureEndpoints_WhenCalled_ShoulReturnInstance()
    {
        var options = new KestrelServerOptions();

        KestrelServerOptionsExtensions.ConfigureEndpoints(options);
        
        options.Should().NotBeNull();
    }
}