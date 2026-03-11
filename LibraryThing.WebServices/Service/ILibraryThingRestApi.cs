using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace LibraryThing.WebServices.Service
{
    internal interface ILibraryThingRestApi
    {
        [Get("/services/rest/1.1/")]
        Task<HttpResponseMessage> GetAsync([Query] IDictionary<string, string> parameters);
    }
}
