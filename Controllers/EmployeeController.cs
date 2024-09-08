using Employeeportal.data;
using Employeeportal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Reflection;
using static Employeeportal.Models.employeeINFO;


namespace Employeeportal.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly dbDATA display;
        public EmployeeController(dbDATA display)
        {
            this.display = display;
        }



        [HttpGet]
        public async Task<IActionResult> EmployeeInfoEntry()

        {
            var dept = await display.departments.ToListAsync();


            var n = new employeeINFO
            {

                //FirstNAME = "",
                //LastNAME = "",
                //DOB = "",
                //Gender = "",
                //MaritalStatus = "",
                //Nationality = "",
                //Photo = "",

                //Email = "",
                //Phone = "",
                //Address = "",
                //EmergencyContact = "",

                ////EmployeeID = "",
                Department = dept,
                //Position = "",
                //DateOfJoining = "",
                //EmployeeType = "",
                //Supervisor = "",

                //SalaryRate = "",
                //PayFrequency = "",
                //BenefitsEligibility = "",

                //BankNAME = "",
                //AccountNumber = "",
                //RoutingNumber = "",

                //CV = "",
                //OfferLetter = "",
                //IdentificationDocuments = "",
                //TaxDocuments = ""


            };

            ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME");


            return View(n);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeInfoEntry(employeeINFO m)
        {

            await display.employeeINFOs.AddAsync(m);
            await display.SaveChangesAsync();

            var dept = await display.departments.ToListAsync();

            ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> EmployeeInfoListUpdate()
        {
            var employees = await (from m in display.employeeINFOs
                                   join d in display.departments on m.departmentID equals d.departmentID
                                   select new employeeINFO
                                   {
                                       FirstNAME = m.FirstNAME,
                                       LastNAME = m.LastNAME,
                                       DOB = m.DOB,
                                       Gender = m.Gender,
                                       MaritalStatus = m.MaritalStatus,
                                       Nationality = m.Nationality,
                                       Photo = m.Photo,

                                       Email = m.Email,
                                       Phone = m.Phone,
                                       Address = m.Address,
                                       EmergencyContact = m.EmergencyContact,

                                       EmployeeID = m.EmployeeID,
                                       departmentNAME = d.departmentNAME,
                                       Position = m.Position,
                                       DateOfJoining = m.DateOfJoining,
                                       EmployeeType = m.EmployeeType,
                                       Supervisor = m.Supervisor,

                                       SalaryRate = m.SalaryRate,
                                       PayFrequency = m.PayFrequency,
                                       BenefitsEligibility = m.BenefitsEligibility,

                                       BankNAME = m.BankNAME,
                                       AccountNumber = m.AccountNumber,
                                       RoutingNumber = m.RoutingNumber,

                                       CV = m.CV,
                                       OfferLetter = m.OfferLetter,
                                       IdentificationDocuments = m.IdentificationDocuments,
                                       TaxDocuments = m.TaxDocuments

                                   }).ToListAsync();

            return View(employees);
        }


        [HttpGet]
        public async Task<IActionResult> EditEmployeeInfo(int id)
        {
            var m = await display.employeeINFOs.FindAsync(id);

            if (m == null)
            {
                return NotFound("DATA NOT FOUND");
            }


            var dept = await display.departments.ToListAsync();

            ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME", m.departmentID);


            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployeeInfo(employeeINFO m)
        {
            if (!ModelState.IsValid)
            {
                var desig = await display.designations.ToListAsync();
                var dept = await display.departments.ToListAsync();

                ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME", m.departmentID);


                return View(m);
            }

            var n = await display.employeeINFOs.FindAsync(m.EmployeeID);

            if (n == null)
            {
                return NotFound();
            }

            n.FirstNAME = m.FirstNAME;
            n.LastNAME = m.LastNAME;
            n.DOB = m.DOB;
            n.Gender = m.Gender;
            n.MaritalStatus = m.MaritalStatus;
            n.Nationality = m.Nationality;
            n.Photo = m.Photo;

            n.Email = m.Email;
            n.Phone = m.Phone;
            n.Address = m.Address;
            n.EmergencyContact = m.EmergencyContact;

            //n.EmployeeID = m.EmployeeID;
            n.departmentID = m.departmentID;
            n.Position = m.Position;
            n.DateOfJoining = m.DateOfJoining;
            n.EmployeeType = m.EmployeeType;
            n.Supervisor = m.Supervisor;

            n.SalaryRate = m.SalaryRate;
            n.PayFrequency = m.PayFrequency;
            n.BenefitsEligibility = m.BenefitsEligibility;

            n.BankNAME = m.BankNAME;
            n.AccountNumber = m.AccountNumber;
            n.RoutingNumber = m.RoutingNumber;

            n.CV = m.CV;
            n.OfferLetter = m.OfferLetter;
            n.IdentificationDocuments = m.IdentificationDocuments;
            n.TaxDocuments = m.TaxDocuments;

            await display.SaveChangesAsync();

            return RedirectToAction("EmployeeInfoListUpdate", "Employee");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployeeInfo(employeeINFO m)
        {
            var n = await display.employeeINFOs.AsNoTracking().FirstOrDefaultAsync(a => a.EmployeeID == m.EmployeeID);

            if (n != null)
            {
                display.employeeINFOs.Remove(n);
                await display.SaveChangesAsync();
            }

            return RedirectToAction("EmployeeInfoListUpdate", "Employee");
        }


        [HttpGet]
        public async Task<IActionResult> PersonalInfoList(int id)
        {
            var m = await display.employeeINFOs.FindAsync(id);

            if (m == null)
            {
                return NotFound("DATA NOT FOUND");
            }


            var dept = await display.departments.ToListAsync();

            ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME", m.departmentID);


            return View(m);
        }



        [HttpPost]
        public async Task<IActionResult> PersonalInfoList(employeeINFO m)
        {
            if (!ModelState.IsValid)
            {
                var desig = await display.designations.ToListAsync();
                var dept = await display.departments.ToListAsync();

                ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME", m.departmentID);


                return View(m);
            }

            var n = await display.employeeINFOs.FindAsync(m.EmployeeID);

            if (n == null)
            {
                return NotFound();
            }

            n.FirstNAME = m.FirstNAME;
            n.LastNAME = m.LastNAME;
            n.DOB = m.DOB;
            n.Gender = m.Gender;
            n.MaritalStatus = m.MaritalStatus;
            n.Nationality = m.Nationality;
            n.Photo = m.Photo;

            n.Email = m.Email;
            n.Phone = m.Phone;
            n.Address = m.Address;
            n.EmergencyContact = m.EmergencyContact;

            //n.EmployeeID = m.EmployeeID;
            n.departmentID = m.departmentID;
            n.Position = m.Position;
            n.DateOfJoining = m.DateOfJoining;
            n.EmployeeType = m.EmployeeType;
            n.Supervisor = m.Supervisor;

            n.SalaryRate = m.SalaryRate;
            n.PayFrequency = m.PayFrequency;
            n.BenefitsEligibility = m.BenefitsEligibility;

            n.BankNAME = m.BankNAME;
            n.AccountNumber = m.AccountNumber;
            n.RoutingNumber = m.RoutingNumber;

            n.CV = m.CV;
            n.OfferLetter = m.OfferLetter;
            n.IdentificationDocuments = m.IdentificationDocuments;
            n.TaxDocuments = m.TaxDocuments;

            await display.SaveChangesAsync();

            return RedirectToAction("PersonalInfoList", "Employee");
        }

    }
}

