using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneralApplication.HelperClasses
{
    public class PageURL
    {

        public enum PageURLS
        {
            MainDashboard = 0,
            Maintenance = 1,
            ClientSearch = 2,
            ReportDashboard = 3,
            ReportMaker = 4
        }



        public string ReturnPageURL(PageURLS p)
        {

            string pagename = "";


            switch(p)
            {
                case PageURLS.MainDashboard:
                    {
                        pagename = "/Dashboards/MainDashboard.aspx";
                        break;
                    }
                case PageURLS.Maintenance:
                    {
                        pagename = "/Dashboards/Maintenance.aspx";
                        break;
                    }
                case PageURLS.ClientSearch:
                    {
                        pagename = "/Clients/ClientSearch.aspx";
                        break;
                    }
                case PageURLS.ReportDashboard:
                    {
                        pagename = "/Dashboards/ReportDashboard.aspx";
                        break;
                    }
                case PageURLS.ReportMaker:
                    {
                        pagename = "/Reports/ReportMaker.aspx";
                        break;
                    }

            }

            return pagename;
        }
    }
}