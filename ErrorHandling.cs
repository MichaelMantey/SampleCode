using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeneralApplication.HelperClasses;

namespace GeneralApplication.HelperClasses
{
    public class ErrorHandling
    {
        Communications com = new Communications();


        public void HandleException(string msg)
        {


            try
            {

                // this will write to db first

                // now send email to developer
                com.SendEmail(false, false, true, msg, "");
               

            }catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                HandleErrorFromError("Communication.cs ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }
        }


        private void HandleErrorFromError(string message)
        {

        }

    }
}