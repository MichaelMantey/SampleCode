using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeneralApplication.Classes;
using GeneralApplication.HelperClasses;

namespace GeneralApplication.Classes
{
    public class Locations
    {

        bool isActive = false;
        bool isSuccess = false;

        DaikokuEntities1 data = new DaikokuEntities1();
        ErrorHandling EH = new ErrorHandling();

        public int ID { get; set; }
        public Guid UI { get; set; }
        public string Street { get; set; }
        public string AptNum { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid AddedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        public int CityID { get; set; }
        public string CityName { get; set; }

        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }

        public bool Active { get; set; }

        public bool MakeActive { get; set; }


        public Locations() { }

        /// <summary>
        /// Checks to see if there is an active location for the current users
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>returns true as a warning </returns>
        public bool CheckForActiveLocation(Guid uid)
        {

            try
            {
                var query = (from a in data.Locations
                             where a.uiresource == uid &&
                             a.active == true
                             select a).FirstOrDefault();

                if (query != null)
                {
                    isActive = true;
                };


            } catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Locations.cs ||| CheckForActiveLocation ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return isActive;
        }

        public bool AddLocation(Locations _location)
        {
            Location l = new Location();

            try
            {
                // make old one inactive first
                if (_location.MakeActive)
                {
                    var query = (from a in data.Locations
                                 where a.uiresource == _location.UI &&
                                 a.active == true
                                 select a).FirstOrDefault();

                    query.active = false;
                    data.SaveChanges();
                }

                l.uiresource = _location.UI;
                l.addressname = _location.Street;
                l.aptnum = _location.AptNum;
                l.city = _location.CityID;
                l.province = _location.ProvinceID;
                l.postalcode = _location.PostalCode;
                l.active = _location.Active;
                l.DateAdded = DateTime.Now;
                l.AddedBy = Sessions.Current.UserData.UserID;


                data.Locations.Add(l);
                data.SaveChanges();

                isSuccess = true;

            } catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Locations.cs ||| AddLocation ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }
            return isSuccess;
        }

        public bool UpdateAddress(Locations _location)
        {

            try
            {
                var query = (from a in data.Locations
                             where a.uiresource == _location.UI &&
                             a.active == true
                             select a).FirstOrDefault();

                query.addressname = (query.addressname == _location.Street) ? query.addressname : _location.Street;
                query.aptnum = (query.aptnum == _location.AptNum) ? query.aptnum : _location.AptNum;
                query.city = (Convert.ToInt32(query.city) == Convert.ToInt32(_location.CityID)) ? Convert.ToInt32(query.city) : Convert.ToInt32(_location.CityID);
                query.province = (Convert.ToInt32(query.province) == Convert.ToInt32(_location.ProvinceID)) ? Convert.ToInt32(query.province) : Convert.ToInt32(_location.ProvinceID);
                query.postalcode = (query.postalcode == _location.PostalCode) ? query.postalcode : _location.PostalCode;
                query.DateUpdated = DateTime.Now;
                query.UpdatedBy = Sessions.Current.UserData.UserID;

                data.SaveChanges();

                isSuccess = true;
            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Locations.cs ||| UpdateAddress ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }
            return isSuccess;
        }

        public Locations GetCurrentClientLocation(Guid ClientID)
        {
            Locations _location = new Locations();

            try
            {
                var query = (from a in data.Locations
                             where a.uiresource == ClientID
                             && a.active == true
                             select a).FirstOrDefault();

                _location.ID = query.id;
                _location.Street = query.addressname;
            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Locations.cs ||| UpdateAddress ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return _location;

        }
          
    }
}