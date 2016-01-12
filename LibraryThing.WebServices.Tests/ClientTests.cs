using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryThing.WebServices.Tests
{
    [TestClass]
    public class ClientTests
    {
        private string ApiKey { get; set; }

        [TestInitialize]
        public void Setup()
        {
            ApiKey = "test"; //TODO - enter your own key.
        }

        [TestMethod]
        public async Task TestGetWorkById()
        {
            var client = new Client(ApiKey);

            var work = await client.GetWorkById(1060); // Jonathan Strange & Mr. Norrell

            Assert.IsNotNull(work);
        }

        [TestMethod]
        public async Task ParseXmlWorkResponse()
        {
            var rnd = new Random();
            var port = (int)(rnd.NextDouble() * (short.MaxValue - 1024) + 1024);
            var uri = $"http://localhost:{port}/librarything/";

            using (var server = new FakeWebServer(new[] {uri}, SendResponse, "application/xml"))
            {
                server.Run();

                var client = new Client(ApiKey, uri);

                var response = await client.GetWorkById(1060);

                Assert.IsNotNull(response);
            }   
        }

        private static string SendResponse(HttpListenerRequest request)
        {
            string xml;

            using (var reader = ReadInputFile("work-1060.xml"))
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
