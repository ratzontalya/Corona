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
        IEnumerable<Patient> GetPatients();
        Patient GetPatientById(int id);
        bool DeletePatient(int id);
        bool UpdatePatient(Patient patient);
        bool UpdateVaccineOfPatient(Vaccine vaccine);
        String GetManufacturerName(int id);
        List<Vaccine> getVaccinesOfPatient(int id);
        bool CreatePatient(Patient patient);
        Vaccine CreateVaccineForPatient(Vaccine vaccine);
        List<int> GetSickPatientsInLastMonth();
    }
}
