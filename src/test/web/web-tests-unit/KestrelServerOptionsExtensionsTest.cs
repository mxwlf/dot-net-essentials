using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Grumpydev.Net.Essentials.Web;
using FluentAssertions;
using NSubstitute;


[TestClass]
public class KestrelServerOptionsExtensionsTest
{
    [TestMethod]
    public void ConfigureEndpoints_WhenCalled_ShoulReturnInstance()
    {
        // Arrange.
        var options = new KestrelServerOptions();
        var configuration = Substitute.For<IConfiguration>();

        // Act.
        KestrelServerOptionsExtensions.ConfigureEndpoints(options, configuration);
        

        // Assert.
        options.Should().NotBeNull();
    }

    public void ConfigureEndpoints_WhenSingleHttpEndpintConfigured_ShouldReturnConfiguration()
    {
        var configuration = Substitute.For<IConfiguration>();


    }
}