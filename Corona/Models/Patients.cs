using BE;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Corona.Models
{
    public class PatientViewModel
    {
        public Patient patient;

        public PatientViewModel(Patient _patient)
        {
            patient = _patient;
        }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "required field")]
        public string firstName
        {
            get { return patient.firstName; }
            set { patient.firstName = value; }
        }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "required field")]
        public string lastName
        {
            get { return patient.lastName; }
            set { patient.lastName = value; }
        }
        public string profileImage
        {
            get { return patient.profileImage; }
            set { patient.profileImage = value; }
        }
        [DisplayName("Phone")]
        [Required(ErrorMessage = "required field")]
        public string phone
        {
            get { return patient.phone; }
            set { patient.phone = value; }
        }
        [DisplayName("Telephone")]
        public string telephone
        {
            get { return patient.telephone; }
            set { patient.telephone = value; }
        }
        [DisplayName("Address")]
        [Required(ErrorMessage = "required field")]
        [RegularExpression(@"[a-zA-Z ]+[,][a-zA-Z ]+[0-9]+", ErrorMessage = "Enter a valid address as: Town, Street House Number")]
        public string address
        {
            get { return patient.address; }
            set { patient.address = value; }
        }
        [DisplayName("Birth Date")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime birthDate
        {
            get { return patient.birthDate; }
            set { patient.birthDate = value; }
        }

        [DisplayName("Sick Date")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        public DateTime sickDate
        {
            get { return patient.sickDate; }
            set { patient.sickDate = value; }
        }
        [DisplayName("Recovery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        public DateTime recoveryDate
        {
            get { return patient.recoveryDate; }
            set { patient.recoveryDate = value; }
        }
        [Required(ErrorMessage = "required field")]
        public int id
        {
            get { return patient.id; }
            set { patient.id = value; }
        }
    }
}