using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace GeneralApplication.HelperClasses
{
    public class CSV
    {
        // class declarations
        ErrorHandling EH = new ErrorHandling();

        // required variables
        private DataTable ReturnValue = new DataTable();


        // methods
        public DataTable ReadCSV(string filePath)
        {


            try
            {
                FileInfo file = new FileInfo(filePath);
                using (OleDbConnection con =
                        new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" +
                        file.DirectoryName + "\";Extended Properties='text;HDR=Yes;FMT=Delimited(,)';"))
                {
                    using (OleDbCommand cmd = new OleDbCommand(string.Format
                                              ("SELECT * FROM [{0}]", file.Name), con))
                    {
                        con.Open();
                        using (OleDbDataAdapter adp = new OleDbDataAdapter(cmd))
                        {
                            adp.Fill(ReturnValue);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("CSV.cs ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return ReturnValue;
        }

        public void WriteToCSV(DataTable dt, string ReturnCSVPath)
        {
            var lines = new List<string>();

            try
            {
                

                string[] columnNames = dt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName).
                                                  ToArray();

                var header = string.Join(",", columnNames);
                lines.Add(header);

                var valueLines = dt.AsEnumerable()
                                   .Select(row => string.Join(",", row.ItemArray));
                lines.AddRange(valueLines);

                


            }catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("CSV.cs ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            File.WriteAllLines(ReturnCSVPath, lines);

        }
    }
}