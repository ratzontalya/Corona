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
        #region SELECT
        public IEnumerable<Patient> GetPatients()
        {
            return dal.GetPatients();
        }
        public Patient GetPatientById(int id, bool active = true)
        {
            return dal.GetPatientById(id, active);
        }
        public List<Vaccine> GetVaccinesOfPatient(int id)
        {
            return dal.GetVaccinesOfPatient(id);
        }
        public int GetUnVaccinedPatients()
        {
            return dal.GetUnVaccinedPatients();
        }
        #endregion
        #region DELETE
        public bool DeletePatient(int id)
        {
            return dal.DeletePatient(id);
        }
        #endregion
        #region UPDATE
        public bool UpdatePatient(Patient patient)
        {
            return dal.UpdatePatient(patient);
        }
        public bool UpdateVaccineOfPatient(Vaccine vaccine)
        {
            return dal.UpdateVaccineOfPatient(vaccine);
        }
        public bool RecoverPatient(int id)
        {
            return dal.RecoverPatient(id);
        }
        #endregion
        #region CREATE
        public bool CreatePatient(Patient patient)
        {
            return dal.CreatePatient(patient);
        }
        public Vaccine CreateVaccineForPatient(Vaccine vaccine)
        {
            return dal.CreateVaccineForPatient(vaccine);
        }
        #endregion
        #region GRAPH
        public List<int> GetSickPatientsInLastMonth()
        {
            return dal.GetSickPatientsInLastMonth();
        }
        #endregion
    }
}
