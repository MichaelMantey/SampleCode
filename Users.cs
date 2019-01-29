using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneralApplication.HelperClasses
{
    public class Users
    {

        // class declaration
        ErrorHandling EH = new ErrorHandling();

        public Guid UserID { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserFullName
        {
            get
            {
                return this.UserFirstName + " " + this.UserLastName;
            }
            set
            {
                
            }
        }

        public int UserRoleID { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }


        public Users(Guid userid,string userfirstname, string userlastname)
        {

        }


        public Users() { }

        public Users ValidateLogin(string username, string password)
        {

            Users userdata = new Users();          

            try
            {
                userdata.UserFirstName = "Mike";
                userdata.UserLastName = "Mantey";
                userdata.UserID = Guid.Parse("c880e2e4-6b83-44df-9e7e-5bf1a0ca1533");
                userdata.UserRoleID = 100;
                userdata.IsActive = true;
                userdata.IsLocked = false;


                /// set valid login
                /// 

                Sessions.Current.LoginValid = true;

            }catch(Exception ex)
            {
                Sessions.Current.ExceptionCount++;
                EH.HandleException("Users.cs ||| " + ex.StackTrace.ToString() + " ||| " + ex.Message);
            }

            return userdata;
        }
    }
}