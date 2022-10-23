using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BL:IBL
    {
        IDAL dal = new DAL.DAL();
        public IEnumerable<Patient> GetPatients()
        {
            return dal.GetPatients();
        }
        public Patient GetPatientById(int id)
        {
            return dal.GetPatientById(id);
        }
        public bool DeletePatient(int id)
        {
            return dal.DeletePatient(id);
        }
        public bool UpdatePatient(Patient patient)
        {
            return dal.UpdatePatient(patient);
        }
        public String GetManufacturerName(int id)
        {
            return dal.GetManufacturerName(id);
        }
        public bool UpdateVaccineOfPatient(Vaccine vaccine)
        {
            return dal.UpdateVaccineOfPatient(vaccine);
        }
        public List<Vaccine> getVaccinesOfPatient(int id)
        {
            return dal.getVaccinesOfPatient(id);
        }
        public bool CreatePatient(Patient patient)
        {
            return dal.CreatePatient(patient);
        }
        public Vaccine CreateVaccineForPatient(Vaccine vaccine)
        {
            return dal.CreateVaccineForPatient(vaccine);
        }
        public List<int> GetSickPatientsInLastMonth()
        {
            return dal.GetSickPatientsInLastMonth();
        }
    }
}
