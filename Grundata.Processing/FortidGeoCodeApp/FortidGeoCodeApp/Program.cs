using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Net;
using System.IO;

namespace ValdagGeoCodeApp
{
    class Program
    {
        /// <summary>
        /// Reads the vallokal förtidsröstning excelfile and get geographical coordinates from Google maps
        /// Also creates a "clean" adress from the adress in the input file
        /// This application writes new column data to the output file , Note that an empty output file should exist (with column names) before starting the process
        /// The output file will be used as one of the source files for the "VallokalerFortid" table in Azure storage
        /// </summary>
        static void Main(string[] args)
        {
            string fileToRead = ConfigurationManager.AppSettings.Get("in_file_path");

            List<ResultData> resultList = new List<ResultData>();

            //Connect to input file
            var connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileToRead + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                //Get the Sheet name
                DataTable sheets = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string worksheet = string.Empty;
                if (sheets != null)
                    foreach (DataRow dr in sheets.Rows)
                    {
                        worksheet = dr[2].ToString();
                        break;
                    }

                //Select the data and save in a list
                var query = "SELECT * FROM " + "[" + worksheet + "]";
                var command = new OleDbCommand(query, connection);
                using (var dr = command.ExecuteReader())
                {
                    while (dr != null && dr.Read())
                    {
                        ResultData data = new ResultData();
                        data.vallokal_id = dr.GetValue(0).ToString();
                        data.vallokalNamn = dr.GetValue(1).ToString();
                        data.adress = dr.GetValue(2).ToString();
                        data.postNr = dr.GetValue(3).ToString();
                        data.postOrt = dr.GetValue(4).ToString();
                        resultList.Add(data);
                    }
                }
            }
            Console.WriteLine("Reading data success; Total rows: " + resultList.Count);

            for (int i = 0; i < resultList.Count; i++)
            {
                //clean the adress
                //change the separators based on input adress fields
                string[] separators = {",","/"," via"," ing","("," ög" };
                string[] str = resultList[i].adress.Split(separators,StringSplitOptions.RemoveEmptyEntries);
                string tempaddress = str[0] + " " + resultList[i].postOrt;

                //Get geo coding
                var lat_lang = GoogleGeoCode(tempaddress);  //calling geocoding method

                if (lat_lang.status == "OK")
                {
                    var lati = Convert.ToString(lat_lang.results[0].geometry.location.lat);
                    var longi = Convert.ToString(lat_lang.results[0].geometry.location.lng);
                    resultList[i].latitud = lati;
                    resultList[i].longitud = longi;
                }
                else
                {
                    resultList[i].latitud = lat_lang.status;
                    resultList[i].longitud = lat_lang.status;
                    Console.WriteLine("Row id: " + i + " " + lat_lang.status);
                }
                //Save the clean adress in out_file
                resultList[i].adress_geo = str[0];
            }
            Console.WriteLine("\nGeocoding Success");
            Console.WriteLine("Press any key to start writing");
            Console.ReadLine();
            Console.WriteLine("Writing data...");
            
            string fileToWrite = ConfigurationManager.AppSettings.Get("out_file_path"); //just folder path for csv; Change accordingly.

            //Connect to output file
            var connectionString_write = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileToWrite + ";Extended Properties='Excel 12.0;HDR=Yes;'";
            using (var connection = new OleDbConnection(connectionString_write))
            {
                connection.Open();

                //Get the Sheet name
                DataTable sheets = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string worksheet = string.Empty;
                if (sheets != null)
                    foreach (DataRow dr in sheets.Rows)
                    {
                        worksheet = dr[2].ToString();
                        break;
                    }

                //Write to output file
                for (int j = 0; j < resultList.Count; j++)
                {
                    var query = "INSERT INTO" + "[" + worksheet + "]" + "([Vallokal_id],[Vallokal_namn],[Adress],[Postnr],[Postort],[Latitud],[Longitud],[Adress_geo]) " + "VALUES(@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8)";

                    var command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@value1", resultList[j].vallokal_id);
                    command.Parameters.AddWithValue("@value2", resultList[j].vallokalNamn);
                    command.Parameters.AddWithValue("@value3", resultList[j].adress);
                    command.Parameters.AddWithValue("@value4", resultList[j].postNr);
                    command.Parameters.AddWithValue("@value5", resultList[j].postOrt);
                    command.Parameters.AddWithValue("@value6", resultList[j].latitud);
                    command.Parameters.AddWithValue("@value7", resultList[j].longitud);
                    command.Parameters.AddWithValue("@value8", resultList[j].adress_geo);

                    command.ExecuteNonQuery();
                    Console.WriteLine("row no " + j);
                }
            }
            Console.WriteLine("\nwriting to file Success\n");
            Console.ReadLine();
        }

        //Fetch data from Google maps api
        public static RootObject GoogleGeoCode(string address)
        {
            string googleMapsApi = ConfigurationManager.AppSettings.Get("google_maps_api");
            string apiKey = ConfigurationManager.AppSettings.Get("api_key");
            string url = string.Format(googleMapsApi + "&key=" + apiKey, address);
            var root = new RootObject();

            var req = (HttpWebRequest)WebRequest.Create(url);

            var res = (HttpWebResponse)req.GetResponse();

            using (var streamreader = new StreamReader(res.GetResponseStream()))
            {
                var result = streamreader.ReadToEnd();

                if (!string.IsNullOrWhiteSpace(result))
                {
                    root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result);
                }
            }
            return root;
        }
    }

    class ResultData
    {
        public string vallokal_id { get; set; }
        public string vallokalNamn { get; set; }
        public string adress { get; set; }
        public string adress_geo { get; set; }
        public string postNr { get; set; }
        public string postOrt { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }

    }

    //Classes to store JSon Results from google api
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class RootObject
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}
