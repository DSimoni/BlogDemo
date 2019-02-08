using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Model;
using Blog.Models;
using Blog.Models.ViewModel;
using Blog.Models.Repository;
using Blog.Encryption;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Blog.Controllers
{
   
   
    public class HomeController : Controller
    {
        

        [Authorize]
        public IActionResult Index(int? id)
        {
            id++;

            IList<Post_CategoryViewModel> Post_CategoryView = new List<Post_CategoryViewModel>();
            try
            {
                  if (id == null)
                    {
                    return View(Post_CategoryView);
                       
                    }
                    else 
                    {
                    using (var unitOfWork = new UnitOfWork(new EFDataContext()))
                    {

                        
                        var Posts = (from s in unitOfWork.Post.GetAll()
                                     join p in unitOfWork.Post_Category.GetAll()
                                     on s.Post_ID equals p.Post_ID
                                     join u in unitOfWork.UserRepository.GetAll()
                                     on s.UserRefID equals u.User_ID
                                     where p.Category_ID == id
                                     orderby s.Created_Time
                                     select new
                                     {
                                         s.Post_ID,
                                         s.Title,
                                         s.Content,
                                         s.Image,
                                         s.Created_Time,
                                         u.Username

                                     }).ToList();

                               Post_CategoryView = Posts.Select(x => new Post_CategoryViewModel()
                                            {
                                              Post_ID = x.Post_ID,
                                              Title = x.Title,
                                              Content = x.Content,
                                              Image = x.Image,
                                              Date = x.Created_Time,
                                              Username = x.Username
                                           }).ToList();

                        return View(Post_CategoryView);

                    }


                }

              
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet]
        public JsonResult JSonCategories()
        {
            List<Category> category = new List<Category>();

            try
            {

                using (var unitOfWork = new UnitOfWork(new EFDataContext()))
                {

                    category = unitOfWork.Categories.GetAll().ToList();

                }

                return Json(category);
            }
            catch
            {
                throw;
            }


        }

       
        public IActionResult Sign_Up()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sign_Up(User user)
        {
            
            try
            {
                using (var unitOfWork = new UnitOfWork(new EFDataContext()))
                {
                    if (ModelState.IsValid)
                    {
                        user.Salt = EncryptionHelper.Encrypt(user.Salt);
                        unitOfWork.UserRepository.Add(user);
                        unitOfWork.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
