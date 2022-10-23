using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        #region SELECT
        IEnumerable<Patient> GetPatients();
        Patient GetPatientById(int id);
        int GetUnVaccinedPatients();
        List<Vaccine> GetVaccinesOfPatient(int id);
        #endregion
        #region DELETE
        bool DeletePatient(int id);
        #endregion
        #region UPDATE
        bool UpdatePatient(Patient patient);
        bool UpdateVaccineOfPatient(Vaccine vaccine);
        #endregion
        #region CREATE
        bool CreatePatient(Patient patient);
        Vaccine CreateVaccineForPatient(Vaccine vaccine);
        #endregion
        #region GRAPH
        List<int> GetSickPatientsInLastMonth();
        #endregion
    }
}
