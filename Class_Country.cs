using GeneralApplication.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneralApplication.Classes
{
    public class Class_Country 
    {
        /// <summary>
        /// this looks after all country items
        /// 
        /// Country  all available Countries in database
        /// Company_Country configured to show countries allowed by the company
        /// 
        /// </summary>

        #region Required Classes

        DaikokuEntities1 data = new DaikokuEntities1();
        ErrorHandling Error = new ErrorHandling();

        #endregion


        #region required variables

        List<Class_Country> CountryList = new List<Class_Country>();

        #endregion

        #region properties

        public int ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public bool ACTIVE { get; set; }
        public int COMPANYID { get; set; }

        #endregion


        #region constructors

        public Class_Country() { }
        public Class_Country(int id, string name)
        {
            this.ID = id;
            this.NAME = name;
        }
        public Class_Country(int id, string code, string name)
        {
            this.ID = id;
            this.CODE = code;
            this.NAME = name;
        }
        public Class_Country(int id, string code, string name, bool active)
        {
            this.ID = id;
            this.CODE = code;
            this.NAME = name;
            this.ACTIVE = active;
        }

        #endregion


        #region public methods

        public List<Class_Country> ReturnAllCountry()
        {

            try
            {
                var items = from a in data.Countries
                             select a;


                foreach(var i in items)
                {
                    CountryList.Add(new Class_Country(i.id, i.code, i.name,Convert.ToBoolean(i.C_active)));
                }
                

            }catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                Error.HandleException("Class_Country.cs ||| ReturnAllCountry() ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return CountryList;
        }

        public  List<Class_Country> ReturnCountryForDropdown(Guid _franchiseid)
        {
            try
            {
                var items = (from a in data.Franchise_Country
                             join b in data.Companies on a.franchiseid equals b.uid
                             join c in data.Countries on a.countryid equals c.id
                             where a.franchiseid == _franchiseid
                             select new 
                             {
                                 ID = Convert.ToInt32(c.id),
                                 Code = c.code.ToString(),
                                 Name = c.name.ToString(),
                                 Active = Convert.ToBoolean(c.C_active),
                             });

                // check to see if they configured this table 
                if(items != null)
                {
                    foreach (var i in items)
                    {
                        CountryList.Add(new Class_Country(i.ID, i.Code, i.Name, Convert.ToBoolean(i.Active)));
                    }
                }else
                {
                    CountryList = ReturnAllCountry();
                }

            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                Error.HandleException("Class_Country.cs ||| ReturnCountryForDropdown() ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return CountryList;
        }

        #endregion

    }

}