using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeneralApplication.HelperClasses;

namespace GeneralApplication.Classes
{
    public class Class_Franchise
    {
        DaikokuEntities1 data = new DaikokuEntities1();

        ErrorHandling EH = new ErrorHandling();

        bool hasSuccess = false;

        public int ID { get; set; }
        public Guid UID { get; set; }
        public Guid UIDResource { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public bool ACTIVE { get; set; }



        public Class_Franchise() { }
        public Class_Franchise(int _id, Guid _uid,  string _code, string _name)
        {
            this.ID = _id;
            this.UID = _uid;
            this.CODE = _code;
            this.NAME = _name;
        }


        public Class_Franchise ReturnFranchiseDetails(Guid _franchiseid)
        {
            Class_Franchise fran = new Class_Franchise();
            try
            {

                var query = (from a in data.Franchises
                             where a.uid == _franchiseid
                             select a).FirstOrDefault();

                fran.ID = query.id;
                fran.UID = query.uid;
                fran.CODE = query.code;
                fran.NAME = query.name;


            }catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Class_Franchise.cs ||| ReturnFranchiseDetails ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return fran;
        }

        public List<Class_Franchise> ReturnAllActiveFranchisesByCompany(Guid _companyid)
        {

            List<Class_Franchise> fran = new List<Class_Franchise>();
            try
            {

                var query = from a in data.Franchises
                            where a.uidresource == _companyid
                            where a.active == true
                            select a;


                foreach(var i in query)
                {
                    fran.Add(new Class_Franchise(i.id,i.uid,i.code,i.name));
                }


            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Class_Franchise.cs ||| ReturnAllActiveFranchisesByCompany ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return fran;
        }

        public List<Class_Franchise> ReturnAllFranchisesByCompany(Guid _companyid)
        {

            List<Class_Franchise> fran = new List<Class_Franchise>();
            try
            {

                var query = from a in data.Franchises
                            where a.uidresource == _companyid
                            select a;


                foreach (var i in query)
                {
                    fran.Add(new Class_Franchise(i.id, i.uid, i.code, i.name));
                }


            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Class_Franchise.cs ||| ReturnAllFranchisesByCompany ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return fran;
        }

        public bool AddNewFranchise(Franchise _franchise)
        {
            Guid FranchiseUID = new Guid();
            Franchise f = new Franchise();

            try
            {
                data.Franchises.Add(_franchise);
                data.SaveChanges();

                FranchiseUID = f.uid;

                hasSuccess = true;

            }catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Class_Franchise.cs ||| AddNewFranchise ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return hasSuccess;
        }

    }

    
}