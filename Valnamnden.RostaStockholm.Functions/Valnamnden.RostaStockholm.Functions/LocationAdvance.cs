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
    public static class LocationAdvance
    {
        /// <summary>
        /// Function to get all the vallokaler for "Förtidsröstning" (Pollingstations for voting in advance)
        /// </summary>
        /// <param name="req"></param>
        /// <param name="inTable"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("VallokalerFortid")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "vallokal/fortid")]HttpRequestMessage req, [Table("LokalerFortid", Connection = "RostaStorage")]IQueryable<LocationEntity> inTable, TraceWriter log)
        {
            CultureInfo Culture = new CultureInfo("sv-SE");
            if (req.GetQueryString("lang") == "en")
                Culture = new CultureInfo("en-US");

            var query = from location in inTable.ToList()
                        select new
                        {
                            id = location.RowKey,
                            adress = location.Adress,
                            adress_geo = location.Adress_geo,
                            postNr = location.Postnr,
                            postOrt = location.Postort,
                            namn = location.Vallokal_namn,
                            lat = location.Latitud,
                            lng = location.Longitud,
                            oppettider = CreateDateTime(location, Culture)
                        };

            var response = req.CreateResponse(HttpStatusCode.OK, query, "application/json");
            response.Headers.Add("Cache-Control", "public, max-age=30");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, OPTIONS");
            response.Headers.Add("X-Node", Environment.MachineName);
            return response;
        }

        private static List<Dictionary<string, object>> CreateDateTime(LocationEntity location, CultureInfo culture)
        {
            var list = new List<Dictionary<string, object>>();
            list.AddRange(new[] {
                ParseDateTime(location.Dag01, culture),
                ParseDateTime(location.Dag02, culture),
                ParseDateTime(location.Dag03, culture),
                ParseDateTime(location.Dag04, culture),
                ParseDateTime(location.Dag05, culture),
                ParseDateTime(location.Dag06, culture),
                ParseDateTime(location.Dag07, culture),
                ParseDateTime(location.Dag08, culture),
                ParseDateTime(location.Dag09, culture),
                ParseDateTime(location.Dag10, culture),
                ParseDateTime(location.Dag11, culture),
                ParseDateTime(location.Dag12, culture),
                ParseDateTime(location.Dag13, culture),
                ParseDateTime(location.Dag14, culture),
                ParseDateTime(location.Dag15, culture),
                ParseDateTime(location.Dag16, culture),
                ParseDateTime(location.Dag17, culture),
                ParseDateTime(location.Dag18, culture),
                ParseDateTime(location.Dag19, culture)
            });
            list.RemoveAll(o => o == null);

            //If all opening hours has passed return the text "The polling station is closed"
            if (list.Count() == 0)
                list.Add(NoOpeningHours(culture));
            return list;
        }
        private static Dictionary<string, object> ParseDateTime(string data, CultureInfo culture)
        {
            Dictionary<string, object> dates = new Dictionary<string, object>();
            var tid = data.Split('/');
            DateTime start;
            DateTime end;
            try
            {
                start = DateTime.Parse(tid[0]);
                end = DateTime.Parse(tid[1]);
                //If the opening hours has passed , return null
                if (end.Date < DateTime.Now.Date)
                    return null;
                //If the polling station have no opening hours for a date return the text "Closed"
                dates.Add("datum", start.ToString("dddd d MMMM", culture));
                if ((start.ToString("HH:mm") == "00:00") && (end.ToString("HH:mm") == "00:00"))
                {
                    if (culture.Name == "sv-SE")
                        dates.Add("tid", "Stängt");
                    else
                        dates.Add("tid", "Closed");
                }
                else
                    dates.Add("tid", start.ToString("HH:mm", culture) + "-" + end.ToString("HH:mm", culture));
                dates.Add("idag", (start.Date == DateTime.Today));
            }
            //If we cant get a date return no text
            catch
            {
                if (culture.Name == "sv-SE")
                {
                    dates.Add("datum", "");
                    dates.Add("tid", "");
                }
                else
                {
                    dates.Add("datum", "");
                    dates.Add("tid", "");
                }
            }
            return dates;
        }

        private static Dictionary<string, object> NoOpeningHours(CultureInfo culture)
        {
            Dictionary<string, object> noOpeningText = new Dictionary<string, object>();
            if (culture.Name == "sv-SE")
                noOpeningText.Add("datum","Vallokalen stängd");
            else
                noOpeningText.Add("datum","The polling station is closed");
            noOpeningText.Add("tid", "");
            return noOpeningText;
        }

        public class LocationEntity : TableEntity
        {
            public string Adress { get; set; }

            public string Adress_geo { get; set; }

            public string Postnr { get; set; }
            public string Postort { get; set; }
            public string Vallokal_namn { get; set; }
            public double Latitud { get; set; }
            public double Longitud { get; set; }

            public string Dag01 { get; set; }
            public string Dag02 { get; set; }
            public string Dag03 { get; set; }
            public string Dag04 { get; set; }
            public string Dag05 { get; set; }
            public string Dag06 { get; set; }
            public string Dag07 { get; set; }
            public string Dag08 { get; set; }
            public string Dag09 { get; set; }
            public string Dag10 { get; set; }
            public string Dag11 { get; set; }
            public string Dag12 { get; set; }
            public string Dag13 { get; set; }
            public string Dag14 { get; set; }
            public string Dag15 { get; set; }
            public string Dag16 { get; set; }
            public string Dag17 { get; set; }
            public string Dag18 { get; set; }
            public string Dag19 { get; set; }
        }
    }
}
