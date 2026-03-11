using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryThing.WebServices.Model;
using LibraryThing.WebServices.Service;

namespace LibraryThing.WebServices
{
    public class Client
    {
        private readonly CommonKnowledgeService _ckService;
        private readonly LocalService _localService;

        public Client(string apiKey)
            : this(apiKey, "http://www.librarything.com")
        {
        }

        public Client(string apiKey, string endpoint)
        {
            _ckService = new CommonKnowledgeService(apiKey, endpoint);
            _localService = new LocalService(apiKey, endpoint);
        }

        public Task<WorkItem> GetWorkById(int id) => _ckService.GetWorkById(id);

        public Task<VenueItem> GetVenueById(int id) => _localService.GetVenueById(id);

        public Task<IList<VenueItem>> GetVenuesNearId(int id) => _localService.GetVenuesNearId(id);
    }
}
