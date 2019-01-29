using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace GeneralApplication.HelperClasses
{
    public class Communications
    {

        ErrorHandling EH = new ErrorHandling();



        public void SendEmail(bool Reminder, bool advertize, bool bugreport, string bugmessage, string user)
        {
            string sendto = "";
            string subject = "";
            string body = "";
            string displayname = "";

            DataTable sendlist = new DataTable();
            sendlist.Columns.Add("email");
            sendlist.Columns.Add("displayname");
            sendlist.Columns.Add("subject");
            sendlist.Columns.Add("body");

            try
            {

                if( Reminder)
                {
                    // get list of due loans
                    // set subject line and body 
                }

                if(advertize)
                {
                    // get list of advertize people
                    // set subject line and body 
                }

                if(bugreport)
                {
                    DataRow dr = sendlist.NewRow();
                    dr["email"] = "mikemantey@cogeco.ca";
                    dr["displayname"] = "Mike Mantey";
                    dr["subject"] = "Feature Request";
                    if(!string.IsNullOrEmpty(bugmessage))
                    {
                        dr["body"] = "Feature Requested by " + Sessions.Current.UserName + "  -  on " + DateTime.Now + "  -  " +  bugmessage;
                    }
                    else
                    {
                        dr["body"] = "Feature requested by , on page , on this date";
                    }

                    sendlist.Rows.Add(dr);
                }

                if (!String.IsNullOrEmpty(user))
                {
                    // get user email address
                    // set subject line and body 
                }

                foreach (DataRow dr in sendlist.Rows)
                {
                    sendto = dr["email"].ToString();
                    displayname = dr["displayname"].ToString();
                    subject = dr["subject"].ToString();
                    body = dr["body"].ToString();

                    MailMessage mail = new MailMessage("diakoku2018@gmail.com", sendto);
                    SmtpClient client = new SmtpClient();
                    NetworkCredential cred = new NetworkCredential("diakoku2018@gmail.com", "Diakoku2018!");

                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Port = 587;
                    client.Credentials = cred;
                    client.EnableSsl = true;
                    client.Host = "smtp.gmail.com";
                    mail.Subject = subject;
                    mail.Body = body;
                    client.Send(mail);

                }

               


            }
            catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Communication.cs ||| SendEmail ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

        }


        public void SendTextMessage()
        {

        }



    }
}