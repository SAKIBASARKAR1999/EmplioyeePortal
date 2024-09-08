using Employeeportal.data;
using Employeeportal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace Employeeportal.Controllers
{
    public class Account : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public Account(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogIn model)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.Remember, false);

                if (model.UserName == "sakiba@gmail.com")
                {
                    if (model.Password == "87654321")
                    {
                        return RedirectToAction("EmployeeInfoListUpdate", "Employee");
                    }

                }


                //if (model.UserName ==  )
                //{  
                //  return RedirectToAction("PersonalInfoList", "Employee");
                //}


                if (result.Succeeded)
                {
                    return RedirectToAction("EmployeeInfoEntry", "Employee");
                }
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);

            }

            return View(model);
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {

            if (ModelState.IsValid)
            {
                User u1 = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    Address = model.Address

                };

                var result = await userManager.CreateAsync(u1, model.Password!);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(u1, false);
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }
}
