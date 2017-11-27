using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortidsOppettiderApp
{
    class Program
    {
        /// <summary>
        /// Reads the Öppettider excelfile and creates the correct format for Azure in the output file
        /// This application writes new column data to the output file , Note that an empty output file should exist (with column names) before starting the process
        /// The output file will be used as one of the source files for the "VallokalerFortid" table in Azure storage
        /// </summary>
        static void Main(string[] args)
        {
            string fileToRead = ConfigurationManager.AppSettings.Get("in_file_path");

            List<ResultData> resultList = new List<ResultData>();

            //Connect to input file
            var connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileToRead + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1;'";
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
                        data.vallokalId = dr.GetValue(0).ToString();
                        data.vallokalNamn = dr.GetValue(1).ToString();
                        data.day1Start = dr.GetValue(2).ToString();
                        data.day1End = dr.GetValue(3).ToString(); //column D
                        data.day2Start = dr.GetValue(4).ToString();
                        data.day2End = dr.GetValue(5).ToString(); //column F
                        data.day3Start = dr.GetValue(6).ToString();
                        data.day3End = dr.GetValue(7).ToString(); //column H
                        data.day4Start = dr.GetValue(8).ToString();
                        data.day4End = dr.GetValue(9).ToString();//column J
                        data.day5Start = dr.GetValue(10).ToString();
                        data.day5End = dr.GetValue(11).ToString(); //column L
                        data.day6Start = dr.GetValue(12).ToString();
                        data.day6End = dr.GetValue(13).ToString(); //column N
                        data.day7Start = dr.GetValue(14).ToString();
                        data.day7End = dr.GetValue(15).ToString(); //column P
                        data.day8Start = dr.GetValue(16).ToString();
                        data.day8End = dr.GetValue(17).ToString(); //column R
                        data.day9Start = dr.GetValue(18).ToString();
                        data.day9End = dr.GetValue(19).ToString(); //column T
                        data.day10Start = dr.GetValue(20).ToString();
                        data.day10End = dr.GetValue(21).ToString(); //column V
                        data.day11Start = dr.GetValue(22).ToString();
                        data.day11End = dr.GetValue(23).ToString(); //column X
                        data.day12Start = dr.GetValue(24).ToString();
                        data.day12End = dr.GetValue(25).ToString(); //column Z
                        data.day13Start = dr.GetValue(26).ToString();
                        data.day13End = dr.GetValue(27).ToString(); //AB
                        data.day14Start = dr.GetValue(28).ToString();
                        data.day14End = dr.GetValue(29).ToString(); //AD
                        data.day15Start = dr.GetValue(30).ToString();
                        data.day15End = dr.GetValue(31).ToString(); //AF
                        data.day16Start = dr.GetValue(32).ToString();
                        data.day16End = dr.GetValue(33).ToString(); //AH
                        data.day17Start = dr.GetValue(34).ToString();
                        data.day17End = dr.GetValue(35).ToString(); //AJ
                        data.day18Start = dr.GetValue(36).ToString();
                        data.day18End = dr.GetValue(37).ToString(); //AL
                        data.day19Start = dr.GetValue(38).ToString();
                        data.day19End = dr.GetValue(39).ToString(); //AN
                        data.dager = new List<string>();

                        resultList.Add(data);
                    }
                }
            }
            Console.WriteLine("Reading data success; Total rows: " + resultList.Count);

            //Get dates from the first object (they exists as column names in the input file)
            string day1 = resultList[0].day1Start + "T";
            string day2 = resultList[0].day2Start + "T";
            string day3 = resultList[0].day3Start + "T";
            string day4 = resultList[0].day4Start + "T";
            string day5 = resultList[0].day5Start + "T";
            string day6 = resultList[0].day6Start + "T";
            string day7 = resultList[0].day7Start + "T";
            string day8 = resultList[0].day8Start + "T";
            string day9 = resultList[0].day9Start + "T";
            string day10 = resultList[0].day10Start + "T";
            string day11 = resultList[0].day11Start + "T";
            string day12 = resultList[0].day12Start + "T";
            string day13 = resultList[0].day13Start + "T";
            string day14 = resultList[0].day14Start + "T";
            string day15 = resultList[0].day15Start + "T";
            string day16 = resultList[0].day16Start + "T";
            string day17 = resultList[0].day17Start + "T";
            string day18 = resultList[0].day18Start + "T";
            string day19 = resultList[0].day19Start + "T";

            //Format the rest of the rows where every row is a vallokal and its opening hours  
            for (int i = 1; i < resultList.Count; i++) //Formatting time;first object has dates;rest has times
            {
                double val;
                #region day1
                if (double.TryParse(resultList[i].day1Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day1Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day1Start = "00:00:00";

                if (double.TryParse(resultList[i].day1End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day1End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day1End = "00:00:00";
                //final format
                resultList[i].dager.Add(day1 + resultList[i].day1Start + "/" + day1 + resultList[i].day1End); //dag1
                #endregion

                #region day2
                if (double.TryParse(resultList[i].day2Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day2Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day2Start = "00:00:00";

                if (double.TryParse(resultList[i].day2End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day2End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day2End = "00:00:00";
                //final format
                resultList[i].dager.Add(day2 + resultList[i].day2Start + "/" + day2 + resultList[i].day2End);//dag2
                #endregion

                #region day3
                if (double.TryParse(resultList[i].day3Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day3Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day3Start = "00:00:00";

                if (double.TryParse(resultList[i].day3End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day3End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day3End = "00:00:00";

                resultList[i].dager.Add(day3 + resultList[i].day3Start + "/" + day3 + resultList[i].day3End);//dag3
                #endregion

                #region day4
                if (double.TryParse(resultList[i].day4Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day4Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day4Start = "00:00:00";

                if (double.TryParse(resultList[i].day4End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day4End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day4End = "00:00:00";

                resultList[i].dager.Add(day4 + resultList[i].day4Start + "/" + day4 + resultList[i].day4End);//dag4
                #endregion

                #region day5
                if (double.TryParse(resultList[i].day5Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day5Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day5Start = "00:00:00";

                if (double.TryParse(resultList[i].day5End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day5End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day5End = "00:00:00";

                resultList[i].dager.Add(day5 + resultList[i].day5Start + "/" + day5 + resultList[i].day5End);//dag5
                #endregion

                #region day6
                if (double.TryParse(resultList[i].day6Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day6Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day6Start = "00:00:00";

                if (double.TryParse(resultList[i].day6End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day6End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day6End = "00:00:00";

                resultList[i].dager.Add(day6 + resultList[i].day6Start + "/" + day6 + resultList[i].day6End);//dag6
                #endregion

                #region day7
                if (double.TryParse(resultList[i].day7Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day7Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day7Start = "00:00:00";

                if (double.TryParse(resultList[i].day7End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day7End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day7End = "00:00:00";

                resultList[i].dager.Add(day7 + resultList[i].day7Start + "/" + day7 + resultList[i].day7End);//dag7
                #endregion

                #region day8
                if (double.TryParse(resultList[i].day8Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day8Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day8Start = "00:00:00";

                if (double.TryParse(resultList[i].day8End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day8End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day8End = "00:00:00";

                resultList[i].dager.Add(day8 + resultList[i].day8Start + "/" + day8 + resultList[i].day8End);//dag8
                #endregion

                #region day9
                if (double.TryParse(resultList[i].day9Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day9Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day9Start = "00:00:00";

                if (double.TryParse(resultList[i].day9End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day9End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day9End = "00:00:00";

                resultList[i].dager.Add(day9 + resultList[i].day9Start + "/" + day9 + resultList[i].day9End);//dag9
                #endregion

                #region day10
                if (double.TryParse(resultList[i].day10Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day10Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day10Start = "00:00:00";

                if (double.TryParse(resultList[i].day10End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day10End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day10End = "00:00:00";

                resultList[i].dager.Add(day10 + resultList[i].day10Start + "/" + day10 + resultList[i].day10End);//dag10
                #endregion

                #region day11
                if (double.TryParse(resultList[i].day11Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day11Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day11Start = "00:00:00";

                if (double.TryParse(resultList[i].day11End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day11End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day11End = "00:00:00";

                resultList[i].dager.Add(day11 + resultList[i].day11Start + "/" + day11 + resultList[i].day11End);//dag11
                #endregion

                #region day12
                if (double.TryParse(resultList[i].day12Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day12Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day12Start = "00:00:00";

                if (double.TryParse(resultList[i].day12End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day12End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day12End = "00:00:00";

                resultList[i].dager.Add(day12 + resultList[i].day12Start + "/" + day12 + resultList[i].day12End);//dag12
                #endregion

                #region day13
                if (double.TryParse(resultList[i].day13Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day13Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day13Start = "00:00:00";

                if (double.TryParse(resultList[i].day13End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day13End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day13End = "00:00:00";

                resultList[i].dager.Add(day13 + resultList[i].day13Start + "/" + day13 + resultList[i].day13End);//dag13
                #endregion

                #region day14
                if (double.TryParse(resultList[i].day14Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day14Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day14Start = "00:00:00";

                if (double.TryParse(resultList[i].day14End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day14End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day14End = "00:00:00";

                resultList[i].dager.Add(day14 + resultList[i].day14Start + "/" + day14 + resultList[i].day14End);//dag14
                #endregion

                #region day15
                if (double.TryParse(resultList[i].day15Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day15Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day15Start = "00:00:00";

                if (double.TryParse(resultList[i].day15End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day15End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day15End = "00:00:00";

                resultList[i].dager.Add(day15 + resultList[i].day15Start + "/" + day15 + resultList[i].day15End);//dag15
                #endregion

                #region day16
                if (double.TryParse(resultList[i].day16Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day16Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day16Start = "00:00:00";

                if (double.TryParse(resultList[i].day16End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day16End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day16End = "00:00:00";

                resultList[i].dager.Add(day16 + resultList[i].day16Start + "/" + day16 + resultList[i].day16End);//dag16
                #endregion

                #region day17
                if (double.TryParse(resultList[i].day17Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day17Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day17Start = "00:00:00";

                if (double.TryParse(resultList[i].day17End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day17End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day17End = "00:00:00";

                resultList[i].dager.Add(day17 + resultList[i].day17Start + "/" + day17 + resultList[i].day17End);//dag17
                #endregion

                #region day18
                if (double.TryParse(resultList[i].day18Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day18Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day18Start = "00:00:00";

                if (double.TryParse(resultList[i].day18End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day18End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day18End = "00:00:00";

                resultList[i].dager.Add(day18 + resultList[i].day18Start + "/" + day18 + resultList[i].day18End);//dag18
                #endregion

                #region day19
                if (double.TryParse(resultList[i].day19Start, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day19Start = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day19Start = "00:00:00";

                if (double.TryParse(resultList[i].day19End, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                {
                    TimeSpan result = TimeSpan.FromHours(val);
                    resultList[i].day19End = result.ToString("hh':'mm':'ss");
                }
                else resultList[i].day19End = "00:00:00";

                resultList[i].dager.Add(day19 + resultList[i].day19Start + "/" + day19 + resultList[i].day19End);//dag19
                #endregion

            }

            //Write to output excelfile
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

                //Create the rows in the excelfiles with the final format for the opening hours
                for (int j = 1; j < resultList.Count; j++)
                {
                    var query = "INSERT INTO" + "[" + worksheet + "]" + "([Vallokal_id],[Vallokal_namn],"
                    + "[Dag01],[Dag02],[Dag03],[Dag04],[Dag05],[Dag06],[Dag07],[Dag08],[Dag09],[Dag10],[Dag11],[Dag12],[Dag13],[Dag14],[Dag15],[Dag16],[Dag17],[Dag18],[Dag19]) "
                    + "VALUES(@value1, @value2, @value3, @value4, @value5, @value6, @value7,@value8, @value9, @value10, @value11, @value12, @value13, @value14, @value15, @value16, @value17, @value18, @value19, @value20, @value21)";

                    var command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@value1", resultList[j].vallokalId);
                    command.Parameters.AddWithValue("@value2", resultList[j].vallokalNamn);
                    command.Parameters.AddWithValue("@value3", resultList[j].dager[0]);
                    command.Parameters.AddWithValue("@value4", resultList[j].dager[1]);
                    command.Parameters.AddWithValue("@value5", resultList[j].dager[2]);
                    command.Parameters.AddWithValue("@value6", resultList[j].dager[3]);
                    command.Parameters.AddWithValue("@value7", resultList[j].dager[4]);
                    command.Parameters.AddWithValue("@value8", resultList[j].dager[5]);
                    command.Parameters.AddWithValue("@value9", resultList[j].dager[6]);
                    command.Parameters.AddWithValue("@value10", resultList[j].dager[7]);
                    command.Parameters.AddWithValue("@value11", resultList[j].dager[8]);
                    command.Parameters.AddWithValue("@value12", resultList[j].dager[9]);
                    command.Parameters.AddWithValue("@value13", resultList[j].dager[10]);
                    command.Parameters.AddWithValue("@value14", resultList[j].dager[11]);
                    command.Parameters.AddWithValue("@value15", resultList[j].dager[12]);
                    command.Parameters.AddWithValue("@value16", resultList[j].dager[13]);
                    command.Parameters.AddWithValue("@value17", resultList[j].dager[14]);
                    command.Parameters.AddWithValue("@value18", resultList[j].dager[15]);
                    command.Parameters.AddWithValue("@value19", resultList[j].dager[16]);
                    command.Parameters.AddWithValue("@value20", resultList[j].dager[17]);
                    command.Parameters.AddWithValue("@value21", resultList[j].dager[18]);

                    command.ExecuteNonQuery();
                    Console.WriteLine("row no " + j);
                }
            }
            Console.WriteLine("\nwriting to file Success\n");
            Console.ReadLine();

        }
    }
    class ResultData
    {
        public string vallokalId { get; set; }
        public string vallokalNamn { get; set; }
        public string day1Start { get; set; }
        public string day1End { get; set; }
        public string day2Start { get; set; }
        public string day2End { get; set; }
        public string day3Start { get; set; }
        public string day3End { get; set; }
        public string day4Start { get; set; }
        public string day4End { get; set; }
        public string day5Start { get; set; }
        public string day5End { get; set; }
        public string day6Start { get; set; }
        public string day6End { get; set; }
        public string day7Start { get; set; }
        public string day7End { get; set; }
        public string day8Start { get; set; }
        public string day8End { get; set; }
        public string day9Start { get; set; }
        public string day9End { get; set; }
        public string day10Start { get; set; }
        public string day10End { get; set; }
        public string day11Start { get; set; }
        public string day11End { get; set; }
        public string day12Start { get; set; }
        public string day12End { get; set; }
        public string day13Start { get; set; }
        public string day13End { get; set; }
        public string day14Start { get; set; }
        public string day14End { get; set; }
        public string day15Start { get; set; }
        public string day15End { get; set; }
        public string day16Start { get; set; }
        public string day16End { get; set; }
        public string day17Start { get; set; }
        public string day17End { get; set; }
        public string day18Start { get; set; }
        public string day18End { get; set; }
        public string day19Start { get; set; }
        public string day19End { get; set; }
        public List<string> dager { get; set; }
        
    }

}
