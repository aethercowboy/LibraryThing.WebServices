using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryThing.WebServices.Enums;
using LibraryThing.WebServices.Model;
using VenueType = LibraryThing.WebServices.Enums.VenueType;

namespace LibraryThing.WebServices.Service
{
    public class LocalService  : BaseService
    {
        public LocalService(string apiKey, string endpoint) : base(apiKey, endpoint)
        {
        }

        private static string ParentMethod => "librarything.local";

        public static async Task<VenueItem> GetVenue(string apiKey, int? id)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            string method = $"{ParentMethod}.getvenue";

            var parameters = new Dictionary<string, string> { { "method", method }, { "apikey", apiKey } };

            var requestUrl = $"{Api}";

            var atLeastOne = false;

            if (id != null)
            {
                parameters.Add("id", id.ToString());
                atLeastOne = true;
            }

            if (!atLeastOne)
            {
                throw new ArgumentException("Must specify at least one: id.");
            }

            return await GetWebResult<VenueResponse, VenueLtml, VenueItem>(requestUrl, parameters);
        }

        public async Task<VenueItem> GetVenue(int? id)
        {
            return await GetVenue(ApiKey, id);
        }

        public async Task<VenueItem> GetVenueById(int id)
        {
            return await GetVenue(id);
        }

        public static async Task<IList<VenueItem>> GetVenuesNear(string apiKey, string latlon, decimal? lat, decimal? lon, int? id,
            string place, int? distance, Measure? measure, VenueType? venueType)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            string method = $"{ParentMethod}.getvenuesnear";

            var parameters = new Dictionary<string, string> { { "method", method }, { "apikey", apiKey } };

            var requestUrl = $"{Api}";

            var atLeastOne = false;

            if (!string.IsNullOrEmpty(latlon))
            {
                parameters.Add("latlon", latlon);
                atLeastOne = true;
            }

            if (lat != null && lon != null)
            {
                parameters.Add("lat", lat.ToString());
                parameters.Add("lon", lon.ToString());
                atLeastOne = true;
            }

            if (id != null)
            {
                parameters.Add("id", id.ToString());
                atLeastOne = true;
            }

            if (!string.IsNullOrEmpty(place))
            {
                parameters.Add("place", place);
                atLeastOne = true;
            }

            if (!atLeastOne)
            {
                throw new ArgumentException("Must specify at least one: latlon, lat/lon, id, or place.");
            }

            if (distance != null)
            {
                parameters.Add("distance", distance.ToString());
            }

            if (measure != null)
            {
                switch (measure)
                {
                    case Measure.Miles:
                        parameters.Add("measure", ((int)Measure.Miles).ToString());
                        break;
                    case Measure.Kilometers:
                        parameters.Add("measure", ((int)Measure.Kilometers).ToString());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(measure), measure, null);
                }
            }

            if (venueType != null)
            {
                switch (venueType)
                {
                    case VenueType.All:
                        parameters.Add("measure", ((int)VenueType.All).ToString());
                        break;
                    case VenueType.Bookstores:
                        parameters.Add("measure", ((int)VenueType.Bookstores).ToString());
                        break;
                    case VenueType.Libraries:
                        parameters.Add("measure", ((int)VenueType.Libraries).ToString());
                        break;
                    case VenueType.FairsAndFestivals:
                        parameters.Add("measure", ((int)VenueType.FairsAndFestivals).ToString());
                        break;
                    case VenueType.Other:
                        parameters.Add("measure", ((int)VenueType.Other).ToString());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(venueType), venueType, null);
                }
            }

            return await GetWebResultAsList<VenuesResponse, VenuesLtml, VenueItem>(requestUrl, parameters);
        }

        public async Task<IList<VenueItem>> GetVenuesNear(string latlon, decimal? lat, decimal? lon, int? id, string place, int? distance, Measure? measure, VenueType? venueType)
        {
            return await GetVenuesNear(ApiKey, latlon, lat, lon, id, place, distance, measure, venueType);
        }

        public async Task<IList<VenueItem>> GetVenuesNearId(int id)
        {
            return await GetVenuesNear(null, null, null, id, null, null, null, null);
        }

        public static async Task<IList<VenueItem>> SearchVenues(string apiKey, string q, string latlon, decimal? lat, decimal? lon, bool? centerOnQ, Measure? measure)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            string method = $"{ParentMethod}.searchvenues";

            var parameters = new Dictionary<string, string> {{"method", method}, {"apikey", apiKey}};

            var requestUrl = $"{Api}";

            var atLeastOne = false;

            if (!string.IsNullOrEmpty(q))
            {
                parameters.Add("q", q);
                atLeastOne = true;
            }

            if (!atLeastOne)
            {
                throw new ArgumentException("Must specify at least one: q.");
            }

            if (!string.IsNullOrEmpty(latlon))
            {
                parameters.Add("latlon", latlon);
            }

            if (lat != null && lon != null)
            {
                parameters.Add("lat", lat.ToString());
                parameters.Add("lon", lon.ToString());
            }

            if (centerOnQ != null)
            {
                parameters.Add("centeronq", centerOnQ.Value ? 1.ToString() : 0.ToString());
            }

            if (measure != null)
            {
                switch (measure)
                {
                    case Measure.Miles:
                        parameters.Add("measure", ((int) Measure.Miles).ToString());
                        break;
                    case Measure.Kilometers:
                        parameters.Add("measure", ((int) Measure.Kilometers).ToString());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(measure), measure, null);
                }
            }

            return await GetWebResultAsList<VenuesResponse, VenuesLtml, VenueItem>(requestUrl, parameters);
        }

        public static async Task<IList<VenueItem>> GetEventsNear(string apiKey, DateTimeOffset timeStamp, string latlon, decimal? lat, decimal? lon, int? venueId, string place, int? distance, Measure? measure, int? days, int? limit)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            string method = $"{ParentMethod}.geteventsnear";

            var parameters = new Dictionary<string, string>
            {
                {"method", method},
                {"apikey", apiKey},
                {"timestamp", timeStamp.ToString("O")}
            };


            var requestUrl = $"{Api}";

            var atLeastOne = false;

            if (!string.IsNullOrEmpty(latlon))
            {
                parameters.Add("latlon", latlon);
                atLeastOne = true;
            }

            if (lat != null && lon != null)
            {
                parameters.Add("lat", lat.ToString());
                parameters.Add("lon", lon.ToString());
                atLeastOne = true;
            }

            if (venueId != null)
            {
                parameters.Add("venue", venueId.ToString());
                atLeastOne = true;
            }

            if (!string.IsNullOrEmpty(place))
            {
                parameters.Add("place", place);
                atLeastOne = true;
            }

            if (!atLeastOne)
            {
                throw new ArgumentException("Must specify at least one: latlon, lat/lon, venue, or place.");
            }

            if (distance != null)
            {
                parameters.Add("distance", distance.ToString());
            }

            if (measure != null)
            {
                switch (measure)
                {
                    case Measure.Miles:
                        parameters.Add("measure", ((int)Measure.Miles).ToString());
                        break;
                    case Measure.Kilometers:
                        parameters.Add("measure", ((int)Measure.Kilometers).ToString());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(measure), measure, null);
                }
            }

            if (days != null)
            {
                parameters.Add("days", days.ToString());
            }

            if (limit != null)
            {
                parameters.Add("limit", limit.ToString());
            }

            return await GetWebResultAsList<VenuesResponse, VenuesLtml, VenueItem>(requestUrl, parameters);
        }

        public static async Task<IList<EventItem>> SearchEvents(string apiKey, string q, string latlon, decimal? lat, decimal? lon, bool? centerOnQ, Measure? measure)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            string method = $"{ParentMethod}.searchevents";

            var parameters = new Dictionary<string, string>
            {
                {"method", method},
                {"apikey", apiKey},
            };


            var requestUrl = $"{Api}";

            var atLeastOne = false;

            if (!string.IsNullOrEmpty(q))
            {
                parameters.Add("q", q);
                atLeastOne = true;
            }

            if (!atLeastOne)
            {
                throw new ArgumentException("Must specify at least one: id.");
            }

            if (!string.IsNullOrEmpty(latlon))
            {
                parameters.Add("latlon", latlon);
            }

            if (lat != null && lon != null)
            {
                parameters.Add("lat", lat.ToString());
                parameters.Add("lon", lon.ToString());
            }

            if (centerOnQ != null)
            {
                parameters.Add("centeronq", centerOnQ.Value ? 1.ToString() : 0.ToString());
            }

            if (measure != null)
            {
                switch (measure)
                {
                    case Measure.Miles:
                        parameters.Add("measure", ((int)Measure.Miles).ToString());
                        break;
                    case Measure.Kilometers:
                        parameters.Add("measure", ((int)Measure.Kilometers).ToString());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(measure), measure, null);
                }
            }

            return await GetWebResultAsList<EventsResponse, EventsLtml, EventItem>(requestUrl, parameters);
        }
    }
}
