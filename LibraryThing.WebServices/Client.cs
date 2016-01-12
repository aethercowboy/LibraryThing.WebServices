using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryThing.WebServices.Model;
using LibraryThing.WebServices.Service;

namespace LibraryThing.WebServices
{
    public class Client
    {
        private string ApiKey { get; }
        private string Endpoint { get; } = "http://www.librarything.com";

        private CommonKnowledgeService _ckService;

        private LocalService _localService;

        private CommonKnowledgeService CommonKnowledgeService => _ckService ?? (_ckService = new CommonKnowledgeService(ApiKey, Endpoint));
        private LocalService LocalService => _localService ?? (_localService = new LocalService(ApiKey, Endpoint));

        public Client(string apiKey)
        {
            ApiKey = apiKey;
        }

        public Client(string apiKey, string endpoint) : this(apiKey)
        {
            Endpoint = endpoint;
        }

        public async Task<WorkItem> GetWorkById(int id)
        {
            return await CommonKnowledgeService.GetWorkById(id);
        }

        public async Task<VenueItem> GetVenueById(int id)
        {
            return await LocalService.GetVenueById(id);
        }

        public async Task<IList<VenueItem>> GetVenuesNearId(int id)
        {
            return await LocalService.GetVenuesNearId(id);
        }
    }
}
