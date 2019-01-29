using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeneralApplication.HelperClasses;

namespace GeneralApplication.Classes
{
    public class Province
    {
        // other classes
        ErrorHandling EH = new ErrorHandling();
        DataConnection DC = new DataConnection();

        DaikokuEntities1 data = new DaikokuEntities1();

        // other things needed
        List<Province> provincelist = new List<Province>();
        bool Success = false;
        string ProvName = "";

        // properties
        public int ID { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }

        //constructors
        public Province() { }

        public Province(int id, string code)
        {
            this.ID = id;
            this.ProvinceCode = code;
        }

        public Province(int id,string code, string name)
        {
            this.ID = id;
            this.ProvinceCode = code;
            this.ProvinceName = name;
        }


        //methods

        public List<Province> GetProvinces()
        {

            try
            {
                


            }catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Province.cs ||| GetProvince() ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return provincelist;
        }

        public bool UpdateProvinceList(Province p)
        {

            try
            {
                Province prov = new Province();
                prov.ProvinceCode = p.ProvinceCode;
                prov.ProvinceName = p.ProvinceName;

                data.SaveChanges();

            }catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Province.cs ||| UpdateProvinceList() ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return Success;
        }

        public string GetProvinceName( int id)
        {

            try
            {

            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Province.cs ||| GetProvince() ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return ProvName;
        }
    }
}