using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BL;
using Corona.Models;

namespace Corona.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IBL bl = new BL.BL();
            var patients = bl.GetPatients();
            List<PatientViewModel> result = new List<PatientViewModel>();
            foreach (Patient item in patients)
            {
                result.Add(new PatientViewModel(item));
            }
            return View(result);
        }
        public ActionResult Edit(int id)
        {
            IBL bl = new BL.BL();
            Patient patient = bl.GetPatientById(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(new PatientViewModel(patient));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPatient(int id, String firstName, String lastName, String phone, String telephone, DateTime sickDate, DateTime recoveryDate, String address, DateTime birthDate,String profileImage)
        {
            try
            {
                if (recoveryDate <= sickDate)
                {
                    ModelState.AddModelError("recoveryDate", "Invalid Date Range");
                    ModelState.AddModelError("sickDate", "Invalid Date Range");
                }
                if(sickDate > DateTime.Now)
                    ModelState.AddModelError("sickDate", "Invalid Sick Date");
                if (birthDate < DateTime.Now.AddYears(-120) || birthDate > DateTime.Now)
                    ModelState.AddModelError("birthDate", "Invalid Birth Date");
                if (birthDate > sickDate)
                    ModelState.AddModelError("sickDate", "Can't be before birth date");
                Patient patient = new Patient
                {
                    active = true,
                    address = address,
                    birthDate = birthDate,
                    firstName = firstName,
                    lastName = lastName,
                    id = id,
                    phone = phone,
                    profileImage = profileImage,
                    recoveryDate = recoveryDate,
                    sickDate = sickDate,
                    telephone = telephone
                };
                if (ModelState.IsValid)
                {
                    IBL bl = new BL.BL();
                    bl.UpdatePatient(patient);
                    ViewBag.message = "Update was successful!";

                    return RedirectToAction("EditVaccines", new { id = id });
                }
                return View("Edit",new PatientViewModel(patient));
            }
            catch (Exception e)
            {
                Session["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult EditVaccines(int id,String messageSuccessful = "", String messageError = "")
        {
            IBL bl = new BL.BL();
            List<VaccineViewModel> vaccineViewModels = new List<VaccineViewModel>();
            List<Vaccine> vaccines = bl.GetVaccinesOfPatient(id);
            foreach (Vaccine item in vaccines)
            {
                vaccineViewModels.Add(new VaccineViewModel(item));
            }
            while(vaccineViewModels.Count() < 4)
            {
                vaccineViewModels.Add(new VaccineViewModel(new Vaccine { id=id}));
            }
            ViewBag.messageSuccessful = messageSuccessful;
            ViewBag.messageError = messageError;
            return View("EditVaccines", vaccineViewModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVaccine(int id, DateTime date, String manufacturer, int autoId)
        {
            try
            {
                IBL bl = new BL.BL();
                Patient patient = bl.GetPatientById(id);
                if (patient != null && patient.birthDate > date)
                {
                    ModelState.AddModelError("date", "Can't be before birth date");
                }
                Vaccine vaccine = new Vaccine
                {
                    id=id,
                    date=date,
                    manufacturer=manufacturer,
                    autoId=autoId
                };
                if (ModelState.IsValid)
                {
                    if (autoId != 0)
                    {
                        bl.UpdateVaccineOfPatient(vaccine);
                    }
                    else
                    {
                        bl.CreateVaccineForPatient(vaccine);
                    }
                    return RedirectToAction("EditVaccines", new { id = id, messageSuccessful= "Update was successful!"} );
                }
                return RedirectToAction("EditVaccines", new { id = id, messageError = "There is an error!" });
            }
            catch (Exception e)
            {
                Session["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult Create(String messageSuccessful = "", String messageError = "")
        {
            ViewBag.messageSuccessful = messageSuccessful;
            ViewBag.messageError = messageError;
            return View(new PatientViewModel(new Patient()));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, String firstName, String lastName, String phone, String telephone, DateTime sickDate, DateTime recoveryDate, String address, DateTime birthDate, String profileImage)
        {
            if (recoveryDate <= sickDate)
            {
                ModelState.AddModelError("recoveryDate", "Invalid Date Range");
                ModelState.AddModelError("sickDate", "Invalid Date Range");
            }
            if (sickDate > DateTime.Now)
                ModelState.AddModelError("sickDate", "Invalid Sick Date");
            if (birthDate < DateTime.Now.AddYears(-120) || birthDate > DateTime.Now)
                ModelState.AddModelError("birthDate", "Invalid Birth Date");
            if (birthDate < sickDate)
                ModelState.AddModelError("sickDate", "Can't be before birth date");
            Patient patient = new Patient
            {
                active = true,
                address = address,
                birthDate = birthDate,
                firstName = firstName,
                lastName = lastName,
                id = id,
                phone = phone,
                profileImage = profileImage,
                recoveryDate = recoveryDate,
                sickDate = sickDate,
                telephone = telephone
            };
            try
            {
                IBL bl = new BL.BL();
                Patient exists = bl.GetPatientById(id);
                if (exists !=null)
                {
                    return RedirectToAction("Create", new { messageError = "There is already patient with that ID" });
                }
                bool res = bl.CreatePatient(patient);
                return RedirectToAction("EditVaccines", new { id = id });
            }
            catch (Exception e)
            {
                return RedirectToAction("Create", new { messageError = "There was an error" });
            }
        }
        public ActionResult Delete(int id)
        {
            IBL bl = new BL.BL();
            bool result = bl.DeletePatient(id);
            if (!result)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int id)
        {
            IBL bl = new BL.BL();
            List<VaccineViewModel> vaccineViewModels = new List<VaccineViewModel>();
            List<Vaccine> vaccines = bl.GetVaccinesOfPatient(id);
            foreach (Vaccine item in vaccines)
            {
                vaccineViewModels.Add(new VaccineViewModel(item));
            }
            return View("Details", vaccineViewModels);
        }
        public ActionResult Graph()
        {
            IBL bl = new BL.BL();
            List<int> res = bl.GetSickPatientsInLastMonth();
            ViewBag.withoutVaccine = bl.GetUnVaccinedPatients();
            return View("Graph", res);
        }

        //public ActionResult AddImage(string id)
        //{
        //    Session["Error"] = "";

        //    IBL bl = new BL.BL();
        //    Medicine medicine = bl.FindMedicineInExcel(id);
        //    if (medicine == null)
        //    {
        //        Session["Message"] = "NDCId not found";
        //        return View("Create");
        //    }
        //    if (bl.GetMedicineById(medicine.NDCId) != null)
        //    {
        //        Session["Message"] = "The medicine already exists";
        //        return View("Create");
        //    }
        //    else
        //        return View("AddImage", new MedicineViewModel(medicine));
        //}
    }
}