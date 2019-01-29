using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneralApplication.Classes
{
    public class Loan
    {

        public int LoanID { get; set; }
        public Guid ClientID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
        public decimal IntRate { get; set; }
        public decimal Fee { get; set; }
          
    }
}