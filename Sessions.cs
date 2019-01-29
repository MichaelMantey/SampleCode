using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeneralApplication.Classes;

namespace GeneralApplication.HelperClasses
{
    public class Sessions
    {


       
        private Sessions()
        {
            ExceptionCount = 0;
            ConnectionString = "";
            UserData = new Users();
            UserName = "";
            ReturnURL = "/Login/Logout.aspx";
            LoginValid = false;
            CurrentURL = "/Login/Logout.aspx";
            CurrentPageName = "";
            CompanyID = -1;
            FranchiseID = -1;
            CurrentClient = null;
        }

        // Gets the current session.
        public static Sessions Current
        {
            get
            {
                Sessions session =
                  (Sessions)HttpContext.Current.Session["__MySession__"];
                if (session == null)
                {
                    session = new Sessions();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }

        public Users UserData { get; set; }
        public string UserName { get; set; }
        public bool LoginValid { get; set; }

        public int ExceptionCount { get; set; }   

        public string ConnectionString { get; set; }

        public string ReturnURL { get; set; }
        public string CurrentURL { get; set; }
        public string CurrentPageName { get; set; }

        public int CompanyID { get; set; }
        public int FranchiseID { get; set; }

        public Classes.Clients CurrentClient { get; set; }
    }
}