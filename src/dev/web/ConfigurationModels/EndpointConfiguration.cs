namespace Grumpydev.Net.Essentials.Web.ConfigurationModels
{
    /// <summary>
    /// Model for binding the endpoint configuration section under HttpServer:Endpoints.
    /// </summary>
    /// Example of section
    /// <remarks>
        /*
            {
            "HttpServer":{
                "Endpoints":{
                    "Http":{
                        "Host": "www.example.org",
                        "Port": 80,
                        "Scheme": "http"
                    },
                    "Https":{
                        "Host": "www.example.org",
                        "Port": 443,
                        "Scheme": "https",
                        "FilePath": "/path/to/certificate"
                    }
                }

            }
            }
        */
    /// </remarks>
    public class EndpointConfiguration
    {
        public string Host { get; set; }

        public int? Port { get; set; }

        public string Scheme { get; set; }
        
        public string FilePath { get; set; }
    }
}