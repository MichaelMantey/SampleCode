using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GeneralApplication.HelperClasses
{
    public class DataConnection
    {

        ErrorHandling EH = new ErrorHandling();

        private DataTable ReturnDataTable = new DataTable();
        private string ReturnString = "";
        private int ReturnINT = -1;
        private DataSet ReturnDataSet = new DataSet();
        
        SqlDataAdapter dataadapter;

        //SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog = DatabaseConnectivity;Trusted_Connection=true;");
        SqlConnection con = new SqlConnection(Sessions.Current.ConnectionString);

        public DataTable ReturnAsDataTable(Hashtable h,string procedurename)
        {


            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedurename;

                SqlDataAdapter dataadapter = new SqlDataAdapter(cmd);

                foreach(DictionaryEntry e in h)  
                {
                    cmd.Parameters.AddWithValue('@' + e.Key.ToString(), e.Value);
                }

                con.Open();

                dataadapter.Fill(ReturnDataTable);


            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("DataConnection.cs ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return ReturnDataTable;
        }

        public string ReturnAsString(Hashtable h, string procedurename)
        {


            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedurename;

                dataadapter = new SqlDataAdapter(cmd);

                foreach (DictionaryEntry e in h)
                {
                    cmd.Parameters.AddWithValue('@' + e.Key.ToString(), e.Value);
                }

                con.Open();

                ReturnString = cmd.ExecuteScalar().ToString();


            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("DataConnection.cs ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return ReturnString;

        }

        public int ReturnAsINT(Hashtable h, string procedurename)
        {


            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedurename;

                dataadapter = new SqlDataAdapter(cmd);

                foreach (DictionaryEntry e in h)
                {
                    cmd.Parameters.AddWithValue('@' + e.Key.ToString(), e.Value);
                }

                con.Open();

                ReturnINT = Convert.ToInt32(cmd.ExecuteScalar());


            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("DataConnection.cs ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return ReturnINT;

        }
    }
}