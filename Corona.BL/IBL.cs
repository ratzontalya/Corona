using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        #region SELECT
        IEnumerable<Patient> GetPatients();
        Patient GetPatientById(int id, bool active = true);
        List<Vaccine> GetVaccinesOfPatient(int id);
        int GetUnVaccinedPatients();
        #endregion
        #region DELETE
        bool DeletePatient(int id);
        #endregion
        #region UPDATE
        bool UpdatePatient(Patient patient);
        bool UpdateVaccineOfPatient(Vaccine vaccine);
        bool RecoverPatient(int id);
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
