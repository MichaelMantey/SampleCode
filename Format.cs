using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneralApplication.HelperClasses
{
    public class Format
    {

         enum MonthString {JAN = 1,FEB,MAR,APR,MAY,JUN,JUL,AUG,SEP,OCT,NOV,DEC}

        public string FormatFirstName(string name)
        {
            char[] letters = name.ToCharArray();

            for (int i = 0; i < letters.Count(); i++)
            {
                if (i == 0)
                {
                    name = letters[i].ToString().ToUpper();
                } else
                {
                    name += letters[i].ToString().ToLower();
                }
            }

            return name;
        }

        public string FormatLastName(string name)
        {
            return name.ToUpper();
        }

        public string HideSIN(string SIN, DateTime CreateDate)
        {
            string ddate = CreateDate.Date.ToShortDateString().Replace("/", "");
            MonthString month = (MonthString)CreateDate.Month;
            int date_hide = Convert.ToInt32(ddate);
            int sin_hide = Convert.ToInt32(SIN);

            return month + (sin_hide + date_hide).ToString();
        }

        public string UnHideSIN(string hideSIN, DateTime CreatedDate)
        {

            string ddate = CreatedDate.Date.ToShortDateString().Replace("/", "");
            int date_hide = Convert.ToInt32(ddate);
            int sin_hide = Convert.ToInt32(hideSIN);

            return (sin_hide - date_hide).ToString().Substring(3);
        }

    }
}