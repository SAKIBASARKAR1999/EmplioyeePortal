using Employeeportal.data;
using Employeeportal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using static Employeeportal.Models.employee;


namespace Employeeportal.Controllers
{
    public class INFO : Controller
    {

        private readonly dbDATA display;
        public INFO(dbDATA display)
        {
            this.display = display;
        }



        //for employee data

        [HttpGet]

        public async Task<IActionResult> EmployeeEntry()

        {
            var desig = await display.designations.ToListAsync();
            var dept = await display.departments.ToListAsync();
            var e1 = new employee
            {

                employeeNAME = "",
                dob = "",
                email = "",
                address = "",
                phone = "",
                Department = dept,
                Designation = desig
            };



            ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME");
            ViewBag.DesignationSelectList = new SelectList(desig, "designationID", "designationNAME");

            return View(e1);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeEntry(employee e)
        {

            await display.employees.AddAsync(e);
            await display.SaveChangesAsync();

            var desig = await display.designations.ToListAsync();
            var dept = await display.departments.ToListAsync();

            ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME");
            ViewBag.DesignationSelectList = new SelectList(desig, "designationID", "designationNAME");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            var employees = await (from e in display.employees
                                   join d in display.departments on e.departmentID equals d.departmentID
                                   join des in display.designations on e.designationID equals des.designationID
                                   select new employee
                                   {
                                       employeeID = e.employeeID,
                                       employeeNAME = e.employeeNAME,
                                       dob = e.dob,
                                       email = e.email,
                                       address = e.address,
                                       designationNAME = des.designationNAME,
                                       departmentNAME = d.departmentNAME
                                   }).ToListAsync();

            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var m = await display.employees.FindAsync(id);

            if (m == null)
            {
                return NotFound("DATA NOT FOUND");
            }

            var desig = await display.designations.ToListAsync();
            var dept = await display.departments.ToListAsync();

            ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME", m.departmentID);
            ViewBag.DesignationSelectList = new SelectList(desig, "designationID", "designationNAME", m.designationID);

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(employee m)
        {
            if (!ModelState.IsValid)
            {
                var desig = await display.designations.ToListAsync();
                var dept = await display.departments.ToListAsync();

                ViewBag.DepartmentSelectList = new SelectList(dept, "departmentID", "departmentNAME", m.departmentID);
                ViewBag.DesignationSelectList = new SelectList(desig, "designationID", "designationNAME", m.designationID);

                return View(m);
            }

            var n = await display.employees.FindAsync(m.employeeID);

            if (n == null)
            {
                return NotFound();
            }

            n.employeeNAME = m.employeeNAME;
            n.email = m.email;
            n.address = m.address;
            n.phone = m.phone;
            n.dob = m.dob;
            n.departmentID = m.departmentID;
            n.designationID = m.designationID;

            await display.SaveChangesAsync();

            return RedirectToAction("EmployeeList", "INFO");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int employeeID)
        {
            var n = await display.employees.AsNoTracking().FirstOrDefaultAsync(a => a.employeeID == employeeID);

            if (n != null)
            {
                display.employees.Remove(n);
                await display.SaveChangesAsync();
            }

            return RedirectToAction("EmployeeList", "INFO");
        }




        //for designation data

        [HttpGet]
        public IActionResult DesignationEntry()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DesignationEntry(designation d)

        {
            var d1 = new designation
            {

                designationID = d.designationID,
                designationNAME = d.designationNAME
            };


            await display.designations.AddAsync(d1);
            await display.SaveChangesAsync();


            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DesignationList()
        {
            var d = await display.designations.ToListAsync();
            return View(d);
        }
        [HttpGet]
        public async Task<IActionResult> EditDesignation(int id)
        {
            var m = await display.designations.FindAsync(id);
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> EditDesignation(designation m)

        {

            var n = await display.designations.FindAsync(m.designationID);

            if (n is not null)

            {
                n.designationID = m.designationID;
                n.designationNAME = m.designationNAME;

                await display.SaveChangesAsync();
            }
            return RedirectToAction("DesignationList", "INFO");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDesignation(designation m)
        {
            var n = await display.designations.AsNoTracking().FirstOrDefaultAsync(a => a.designationID == m.designationID);

            if (n != null)
            {
                display.designations.Remove(n); // Remove the entity retrieved from the database

                await display.SaveChangesAsync();
            }

            return RedirectToAction("DesignationList", "INFO");
        }



        //for departmeantal data

        [HttpGet]
        public IActionResult DepartmentEntry()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DepartmentEntry(department d)

        {
            var d1 = new department
            {

                departmentID = d.departmentID,
                departmentNAME = d.departmentNAME
            };


            await display.departments.AddAsync(d1);
            await display.SaveChangesAsync();


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentList()
        {
            var d = await display.departments.ToListAsync();
            return View(d);
        }

        [HttpGet]
        public async Task<IActionResult> EditDepartment(int id)
        {
            var m = await display.departments.FindAsync(id);
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartment(department m)

        {

            var n = await display.departments.FindAsync(m.departmentID);

            if (n is not null)

            {
                n.departmentID = m.departmentID;
                n.departmentNAME = m.departmentNAME;

                await display.SaveChangesAsync();
            }
            return RedirectToAction("DepartmentList", "INFO");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(department m)
        {
            var n = await display.departments.AsNoTracking().FirstOrDefaultAsync(a => a.departmentID == m.departmentID);

            if (n != null)
            {
                display.departments.Remove(m); // Remove the entity retrieved from the database

                await display.SaveChangesAsync();
            }

            return RedirectToAction("DepartmentList", "INFO");
        }




    }
}

