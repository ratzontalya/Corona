using BE;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Corona.Models
{
    public class VaccineViewModel
    {
        public Vaccine vaccine;

        public VaccineViewModel(Vaccine _vaccine)
        {
            vaccine = _vaccine;
        }
        [DisplayName("Manufacturer")]
        [Required(ErrorMessage = "required field")]
        public String manufacturer
        {
            get { return vaccine.manufacturer; }
            set { vaccine.manufacturer = value; }
        }
        [DisplayName("Vaccine Date")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime date
        {
            get { return vaccine.date; }
            set { vaccine.date = value; }
        }
        [Required(ErrorMessage = "required field")]
        public int id
        {
            get { return vaccine.id; }
            set { vaccine.id = value; }
        }
        [Required(ErrorMessage = "required field")]
        public int autoid
        {
            get { return vaccine.autoId; }
            set { vaccine.autoId = value; }
        }
    }
}