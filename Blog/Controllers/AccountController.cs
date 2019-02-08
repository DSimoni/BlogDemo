using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Model;
using Blog.Models.BusinessLayer;
using Blog.Encryption;
using Blog.Models.Repository;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        public Validate objUser = new Validate();

      

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind] User user)
        {

                ModelState.Remove("FirstName");
                ModelState.Remove("LastName");
                user.Salt = EncryptionHelper.Encrypt(user.Salt);
          
                String LoginStatus = String.Empty;
                var unitOfWork = new UnitOfWork(new EFDataContext());
                
                var status = unitOfWork.UserRepository.GetAll().SingleOrDefault(x => x.Username == user.Username && x.Salt == user.Salt);
                if (status != null)
                {
                    LoginStatus = "True";
                }
                else { LoginStatus = null; }

                    if (!String.IsNullOrEmpty(LoginStatus))
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    };
                        ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(principal);
                       
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                        return View();
                    }
        }


    }
}