using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeneralApplication.HelperClasses;

namespace GeneralApplication.Classes
{
    public class Class_Company
    {

        DaikokuEntities1 data = new DaikokuEntities1();
        ErrorHandling EH = new ErrorHandling();
        

        public int ID { get; set; }
        public Guid UID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public bool ACTIVE { get; set; }


        public Class_Company() { }
        public Class_Company(int id, Guid uid, string code, string name)
        {
            this.ID = id;
            this.CODE = code;
            this.NAME = name;
        }
        public Class_Company(int id, Guid uid, string code, string name, bool active)
        {
            this.ID = id;
            this.CODE = code;
            this.NAME = name;
            this.ACTIVE = active;
        }


        public Class_Company ReturnCompanyDetails()
        {
            Class_Company CompanyDetails = new Class_Company();

            try
            {
                var items = (from a in data.Companies 
                            select a).FirstOrDefault();


                CompanyDetails.ID = items.id;
                CompanyDetails.UID = items.uid;
                CompanyDetails.CODE = items.code;
                CompanyDetails.NAME = items.name;
                
            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Class_Company.cs ||| ReturnCompanyDetails ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return CompanyDetails;
        }
    }

}
