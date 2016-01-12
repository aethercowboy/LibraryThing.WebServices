using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryThing.WebServices.Model;

namespace LibraryThing.WebServices.Service
{
    public class CommonKnowledgeService : BaseService
    {
        public CommonKnowledgeService(string apiKey, string endpoint) : base(apiKey, endpoint)
        {
        }

        private static string ParentMethod => "librarything.ck";

        public async Task<WorkItem> GetWork(int? id, string isbn, string lccn, string oclc, string name)
        {
            return await GetWork(ApiKey, id, isbn, lccn, oclc, name);
        }

        public async Task<WorkItem> GetWorkById(int id)
        {
            return await GetWork(id, null, null, null, null);
        }

        public async Task<WorkItem> GetWorkByIsbn(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
            {
                throw new ArgumentNullException(nameof(isbn));
            }

            return await GetWork(null, isbn, null, null, null);
        }

        public async Task<WorkItem> GetWorkByLccn(string lccn)
        {
            if (string.IsNullOrEmpty(lccn))
            {
                throw new ArgumentNullException(nameof(lccn));
            }
            return await GetWork(null, null, lccn, null, null);
        }

        public async Task<WorkItem> GetWorkByOclc(string oclc)
        {
            if (string.IsNullOrEmpty(oclc))
            {
                throw new ArgumentNullException(nameof(oclc));
            }
            return await GetWork(null, null, null, oclc, null);
        }

        public async Task<WorkItem> GetWorkByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            return await GetWork(null, null, null, null, name);
        }

        public static async Task<WorkItem> GetWork(string apiKey, int? id, string isbn, string lccn, string oclc,
            string name)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            string method = $"{ParentMethod}.getwork";

            var requestUrl = $"{Api}";

            var parameters = new Dictionary<string, string> {{"method", method}, {"apikey", apiKey}};

            var atLeastOne = false;

            if (id != null)
            {
                parameters.Add("id", id.ToString());
                atLeastOne = true;
            }

            if (!string.IsNullOrEmpty(isbn))
            {
                parameters.Add("isbn", isbn);
                atLeastOne = true;
            }

            if (!string.IsNullOrEmpty(lccn))
            {
                parameters.Add("lccn", lccn);
                atLeastOne = true;
            }

            if (!string.IsNullOrEmpty(oclc))
            {
                parameters.Add("oclc", oclc);
                atLeastOne = true;
            }

            if (!string.IsNullOrEmpty(name))
            {
                parameters.Add("name", name);
                atLeastOne = true;
            }

            if (!atLeastOne)
            {
                throw new ArgumentException("Must specify at least one: id, isbn, lccn, oclc, or name.");
            }

            return await GetWebResult<WorkResponse, WorkLtml, WorkItem>(requestUrl, parameters);
        }

        public async Task<AuthorItem> GetAuthor(int? id, string authorCode, string name)
        {
            return await GetAuthor(ApiKey, id, authorCode, name);
        }

        public async Task<AuthorItem> GetAuthorById(int id)
        {
            return await GetAuthor(id, null, null);
        }

        public async Task<AuthorItem> GetAuthorByAuthorCode(string authorCode)
        {
            if (string.IsNullOrEmpty(authorCode))
            {
                throw new ArgumentNullException(nameof(authorCode));
            }
            return await GetAuthor(null, authorCode, null);
        }

        public async Task<AuthorItem> GetAuthorByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            return await GetAuthor(null, null, name);
        }

        public static async Task<AuthorItem> GetAuthor(string apiKey, int? id, string authorCode, string name)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            string method = $"{ParentMethod}.getauthor";

            var parameters = new Dictionary<string, string> { { "method", method }, { "apikey", apiKey } };

            var requestUrl = $"{Api}";

            var atLeastOne = false;

            if (id != null)
            {
                parameters.Add("id", id.ToString());
                atLeastOne = true;
            }

            if (!string.IsNullOrEmpty(authorCode))
            {
                parameters.Add("authorcode", authorCode);
                atLeastOne = true;
            }

            if (!string.IsNullOrEmpty(name))
            {
                parameters.Add("name", name);
                atLeastOne = true;
            }

            if (!atLeastOne)
            {
                throw new ArgumentException("Must specify at least one: id, authorCode, or name.");
            }

            return await GetWebResult<AuthorResponse, AuthorLtml, AuthorItem>(requestUrl, parameters);
        }
    }
}