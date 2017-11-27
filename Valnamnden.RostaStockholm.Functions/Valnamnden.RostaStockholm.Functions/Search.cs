using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Valnamnden.RostaStockholm.Functions.Utils;


namespace Valnamnden.RostaStockholm.Functions
{
    public static class Search
    {
        /// <summary>
        /// Function that return matching adresses for the search string 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="inTable"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("Sok")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sok")]HttpRequestMessage req, [Table("AdressValdistrikt", Connection = "RostaStorage")]IQueryable<Vote> inTable, TraceWriter log)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var query = req.GetQueryString("q");
            if (string.IsNullOrEmpty(query)) return null;

            query = query.ToUpperInvariant();
            var b = inTable
                .Where(x => x.PartitionKey == query.Substring(0, 1))
                .WhereRowKeyStartsWith(query)
                .Select(i => new
                {
                    lokalId = i.Id,
                    adress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(i.RowKey.ToLowerInvariant())
                });

            var response = req.CreateResponse(HttpStatusCode.OK, b.Take(10), "application/json");
            response.Headers.Add("Cache-Control", "public, max-age=30");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, OPTIONS");
            response.Headers.Add("X-Node", Environment.MachineName);
            response.Headers.Add("Server-Timing", $"Query={sw.ElapsedMilliseconds}; Query");
            return response;
        }

        public class Vote : TableEntity
        {
            public string Postnr { get; set; }
            public int Id { get; set; }
        }

    }
}
