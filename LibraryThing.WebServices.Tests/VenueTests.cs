using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using LibraryThing.WebServices;
using LibraryThing.WebServices.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class VenueTests
    {
        private string ApiKey;

        [TestInitialize]
        public void Initialize()
        {
            ApiKey = "test";
        }

        [TestMethod]
        public async Task ParseXmlVenueResponse()
        {
            var rnd = new Random();
            var port = (int)(rnd.NextDouble() * (short.MaxValue - 1024) + 1024);
            var uri = $"http://localhost:{port}/librarything/";

            using (var server = new FakeWebServer(new[] { uri }, SendResponse, "application/xml"))
            {
                server.Run();

                var client = new Client(ApiKey, uri);

                var response = await client.GetVenueById(5465);

                Assert.IsNotNull(response);
            }
        }

        private static string SendResponse(HttpListenerRequest request)
        {
            string xml;

            using (var reader = ReadInputFile("venue-5465.xml"))
            {
                xml = reader.
                    ReadToEnd();
            }
            return xml;
        }

        private static TextReader ReadInputFile(string filename)
        {
            return File.OpenText(filename);
        }
    }
}
