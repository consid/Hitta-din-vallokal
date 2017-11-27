using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Valnamnden.RostaStockholm.Functions.Utils;

namespace Valnamnden.RostaStockholm.Functions
{
    public static class Location
    {
        /// <summary>
        /// Function to get the vallokal (Pollingstation) information by the id for the vallokal
        /// </summary>
        /// <param name="req"></param>
        /// <param name="inTable"></param>
        /// <param name="id"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("VallokalerValdagen")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "vallokal/valdagen/{id}")]HttpRequestMessage req, [Table("VallokalerValdagen", Connection = "RostaStorage")]IQueryable<LocationEntity> inTable, string id, TraceWriter log)
        {
            var query = from location in inTable
                        where location.PartitionKey == id.Substring(0, 1) && location.RowKey == id
                        select new
                        {
                            id = location.RowKey,
                            adress = location.Adress,
                            adress_geo = location.Adress_geo,
                            postNr = location.Postnr,
                            postOrt = location.Postort,
                            namn = location.Vallokal_namn,
                            distrikt = location.Valdistrikt_namn,
                            oppettider = "", //location.Oppettider,
                            lat = location.Latitud,
                            lng = location.Longitud
                        };
            var response = req.CreateResponse(HttpStatusCode.OK, query.FirstOrDefault(), "application/json");
            response.Headers.Add("Cache-Control", "public, max-age=30");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, OPTIONS");
            response.Headers.Add("X-Node", Environment.MachineName);
            return response;
        }

        public class LocationEntity : TableEntity
        {
            public string Adress { get; set; }
            public string Adress_geo{ get; set; }
            public string Oppettider { get; set; }
            public string Postnr { get; set; }
            public string Postort { get; set; }
            public string Valdistrikt_namn { get; set; }
            public string Vallokal_namn { get; set; }
            public double Latitud { get; set; }
            public double Longitud { get; set; }
        }
    }
}
