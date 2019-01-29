using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GeneralApplication.HelperClasses;

namespace GeneralApplication.Classes
{
    public class Clients
    {
        DaikokuEntities1 data = new DaikokuEntities1();
        List<Clients> ClientList = new List<Clients>();
        ErrorHandling EH = new ErrorHandling();

        public int ID { get; set; }
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SIN { get; set; }
        public string PictureURL { get; set; }
        public bool ClientStatus { get; set; }
        public int TransactionCount { get; set; }

        public Locations ClientLocation { get; set; }

        public Clients() { }

        public string ReturnClientName(int id)
        {

            return "Mike Mantey";
        }



        
        public List<Clients> GetClientListReport(bool ShowAll)
        {

            try
            {
                var query = from a in data.ClientDetails
                            select a;

                

            }
            catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Clients.cs ||| GetClientListReport ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return ClientList;
        }

        // TODO  Finish this
        public Clients ReturnClientData(Guid ClientID)
        {

            // set current client information
            Clients client = new Clients();

            try
            {
                var query = (from a in data.ClientDetails
                             join b in data.Locations on a.ClientID equals b.uiresource
                             join c in data.Status on a.ClientStatus equals c.id
                             join d in data.Cities on b.city equals d.id
                             join e in data.Provinces on b.province equals e.id
                             where a.ClientID == ClientID && b.active == true
                             select new
                             {
                                 ID = a.id,
                                 a.ClientID,
                                 a.FirstName,
                                 MiddleName = (string.IsNullOrEmpty(a.MiddleName) ? "" : a.MiddleName),
                                 a.LastName,
                                 DOB = Convert.ToDateTime(a.DateOfBirth),
                                 a.SIN,
                                 a.PictureURL,
                                 Status = c.StatusName,
                                 StatusID = a.Status,
                                 TransactionCount = Convert.ToInt32(a.TransactionCount)
                             }).FirstOrDefault();
              
                // set current contact information


                // set current client information
                client.ClientID = Convert.ToInt32(query.ID);
                client.ClientLocation = ClientLocation.GetCurrentClientLocation(ClientID);

                Sessions.Current.CurrentClient = client;
            }
            catch (Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Clients.cs ||| ReturnClientData ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return client;
          }
   
    }
}