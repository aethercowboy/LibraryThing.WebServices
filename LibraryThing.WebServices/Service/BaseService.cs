using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LibraryThing.WebServices.Common;
using LibraryThing.WebServices.Model;
using Refit;

namespace LibraryThing.WebServices.Service
{
    public abstract class BaseService
    {
        private const string DefaultEndpoint = "http://www.librarything.com";

        protected string ApiKey { get; }

        protected static string Url { get; private set; } = DefaultEndpoint;

        private static ILibraryThingRestApi ApiClient { get; set; }

        protected BaseService(string apiKey)
            : this(apiKey, DefaultEndpoint)
        {
        }

        protected BaseService(string apiKey, string endpoint)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            ApiKey = apiKey;
            Url = string.IsNullOrWhiteSpace(endpoint) ? DefaultEndpoint : endpoint;
            ApiClient = RestService.For<ILibraryThingRestApi>(new HttpClient { BaseAddress = new Uri(Url) });
        }

        private static async Task<TResponse> GetWebResult<TResponse>(IDictionary<string, string> parameters)
            where TResponse : class
        {
            RateLimiters.PerDayRateGate.WaitToProceed();
            RateLimiters.PerSecondRateGate.WaitToProceed();

            using (var response = await ApiClient.GetAsync(parameters).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var serializer = new XmlSerializer(typeof(TResponse));
                var result = serializer.Deserialize(responseStream) as TResponse;

                if (result == null)
                {
                    throw new InvalidDataException("Response body could not be deserialized.");
                }

                return result;
            }
        }

        protected static async Task<List<TItem>> GetWebResultAsList<TResponse, TLtml, TItem>(IDictionary<string, string> parameters)
            where TResponse : Response<TLtml>, new()
            where TLtml : ListLtml<TItem>, new()
            where TItem : Item, new()
        {
            var result = await GetWebResult<TResponse>(parameters).ConfigureAwait(false);
            return result.Ltml.ItemList;
        }

        protected static async Task<TItem> GetWebResult<TResponse, TLtml, TItem>(IDictionary<string, string> parameters)
            where TResponse : Response<TLtml>, new()
            where TLtml : Ltml<TItem>, new()
            where TItem : Item, new()
        {
            var result = await GetWebResult<TResponse>(parameters).ConfigureAwait(false);
            return result.Ltml.Item;
        }
    }
}
