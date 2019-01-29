using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using GeneralApplication.HelperClasses;
using GeneralApplication.Classes;
using Xceed.Words.NET;

namespace GeneralApplication.HelperClasses
{
    public class Reporting
    {
        Classes.Clients client = new Classes.Clients();
        ErrorHandling EH = new ErrorHandling();

        public enum ReportType
        {
            User = 1,
            DailyTransaction = 2
        }


        public void CreateReport(ReportType r,   int? UserID)
        {

            string fileName = HttpContext.Current.Server.MapPath("/Documents/DocXExample.docx");
       
            var doc = DocX.Create(fileName);
            
            // header stuff
            doc.AddHeaders();

            // odd pages
            Header header_odd = doc.Headers.Odd;
            Paragraph p_header_odd = header_odd.InsertParagraph();
            p_header_odd.Append("Company Name ");
            p_header_odd.FontSize(14);
            p_header_odd.Bold();

            Header header_space = doc.Headers.Odd;
            Paragraph p_header_space = header_space.InsertParagraph();
            p_header_space.Append("");
            p_header_space.InsertHorizontalLine();
           

            

            // even pages
           // do same as odd



            
            // Main body of document

            //leave a space 
            Paragraph ps1 = doc.InsertParagraph("");

            /***********************************
           * 
           *  doc title
           *  
           *  ********************************/

            string _p1 = CreatePageTitle(r, UserID);
            Paragraph p1 = doc.InsertParagraph(_p1);
            p1.Color(System.Drawing.Color.Red);

            Paragraph ps = doc.InsertParagraph();
            ps.Append("");

           

            /***********************************
             * 
             *  main doc body 
             *  
             *  ********************************/

            // footer stuff
            doc.AddFooters();

            // odd pages
            Footer footer_odd = doc.Footers.Odd;
            Paragraph p_footer_odd = footer_odd.InsertParagraph();
            p_footer_odd.Append("© 2018 Vector Programming");

            // even pages
            Footer footer_even = doc.Footers.Even;
            Paragraph p_footer_even = footer_even.InsertParagraph();
            p_footer_even.Append("© 2018 Vector Programming");




            // Save to the output directory:
             doc.Save();

            // Open in Word:
            Process.Start("WINWORD.EXE", fileName);


        }


        private string CreatePageTitle(ReportType r, int? UserID)
        {
            StringBuilder sb = new StringBuilder();

            try
            {

                switch(r)
                {
                    case ReportType.User:
                        {
                            sb.Append("User Report for " + client.ReturnClientName(1) );
                            break;
                        }
                    case ReportType.DailyTransaction:
                        {
                            sb.Append("Daily Transaction Report for " + DateTime.Now.Date.ToLongDateString());
                            break;
                        }
                }


            }catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Reporting.cs ||| CreatePageTitle ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }
            
            return sb.ToString();
        }

       


    }
}