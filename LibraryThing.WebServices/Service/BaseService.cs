using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using LibraryThing.WebServices.Common;
using LibraryThing.WebServices.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace LibraryThing.WebServices.Service
{
    public abstract class BaseService
    {
        protected string ApiKey { get; }

        protected static string Url { get; private set; } = "http://www.librarything.com";

        protected static string Api => "services/rest/1.1/";

        private static XmlMediaTypeFormatter XmlFormatter => GlobalConfiguration.Configuration.Formatters.XmlFormatter;

        protected BaseService(string apiKey)
        {
            ApiKey = apiKey;
            XmlFormatter.UseXmlSerializer = true;
        }

        protected BaseService(string apiKey, string endpoint)
            :this(apiKey)
        {
            Url = endpoint;
        }

        private static async Task<TResponse> GetWebResult<TResponse>(string requestUrl, IDictionary<string, string> parameters)
            where TResponse : new()
        {
            try
            {
                var client = new RestClient { BaseUrl = new Uri(Url) };

                client.ClearHandlers();
                client.AddHandler("text/xml", new XmlAttributeDeserializer());
                client.AddHandler("application/xml", new XmlAttributeDeserializer());

                RateLimiters.PerDayRateGate.WaitToProceed();
                RateLimiters.PerSecondRateGate.WaitToProceed();

                var request = new RestRequest(requestUrl, Method.GET);

                foreach (var p in parameters)
                {
                    request.AddQueryParameter(p.Key, p.Value);
                }

                var response = await client.ExecuteTaskAsync<TResponse>(request);

                if (response == null) throw new Exception("Something went wrong.");

                var result = response.Data;

                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            throw new Exception("Something went wrong.");
        }

        protected static async Task<List<TItem>> GetWebResultAsList<TResponse, TLtml, TItem>(string requestUrl,
            IDictionary<string, string> parameters)
            where TResponse : Response<TLtml>, new()
            where TLtml : ListLtml<TItem>, new()
            where TItem : Item, new()
        {
            try
            {
                var result = await GetWebResult<TResponse>(requestUrl, parameters);

                if (result != null)
                {
                    return result.Ltml.ItemList;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            throw new Exception("Something went wrong.");
        }

        protected static async Task<TItem> GetWebResult<TResponse, TLtml, TItem>(string requestUrl, IDictionary<string, string> parameters)
            where TResponse : Response<TLtml>, new()
            where TLtml : Ltml<TItem>, new()
            where TItem : Item, new()
        {
            try
            {
                var result = await GetWebResult<TResponse>(requestUrl, parameters);

                if (result != null)
                {
                    return result.Ltml.Item;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            throw new Exception("Something went wrong.");
        }
    }
}
