using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corona.Models
{
    public static class Extensions
    {
        static public int GetAge(this Patient user)
        {
            // Save today's date.
            var today = DateTime.Today;
            var birthdate = user.birthDate;
            // Calculate the age.
            var age = today.Year - birthdate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (birthdate.Date > today.AddYears(-age))
                age--;
            return age;
        }

        //static public String GetManufacturerName(this Vaccine vaccine)
        //{
        //    IBL bl = new BL.BL();
        //    String name = bl.GetManufacturerName(vaccine.manufacturer);
        //    return name;
        //}
        public static MvcHtmlString DisplayImage(this HtmlHelper html, string imgPath, int size = 150)
        {
            return new MvcHtmlString($"<img src='{imgPath}' height='{size}' width='{size}'/>");
        }
        public static MvcHtmlString DisplayRoundImage(this HtmlHelper html, string imgPath, int size = 150)
        {
            return new MvcHtmlString($"<img  class='round-img' src='{imgPath}' height='{size}' width='{size}'/>");
        }
        public static MvcHtmlString DisplayHeader(this HtmlHelper html, string textForHeader, int size)
        {
            return new MvcHtmlString($"<h{size}>{textForHeader}</h{size}>");
        }
    }
}