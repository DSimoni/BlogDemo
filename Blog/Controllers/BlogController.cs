using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models.ViewModel;
using Blog.Models.BusinessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using DataLayer.Model;
using Blog.Models.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {



        [Authorize]
        public IActionResult Post()
        {

            PostViewModel post = new PostViewModel();
            post.Categories = Validate.PopulateCategories();
            return View(post);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(PostViewModel post)
        {
            IList<Post_Category> post_Category = new List<Post_Category>();
            IList<SelectListItem> selectedItems = new List<SelectListItem>();
            using (UnitOfWork unitOfWork = new UnitOfWork(new EFDataContext()))
            {
                if (ModelState.IsValid)
                {
                    var validImageTypes = new string[]
                 {
                      "image/gif",
                      "image/jpeg",
                      "image/pjpeg",
                      "image/png"
                   };

                    if (post.ImageUpload == null || post.ImageUpload.Length == 0)
                    {
                        ModelState.AddModelError("ImageUpload", "This field is required");
                    }
                    else if (!validImageTypes.Contains(post.ImageUpload.ContentType))
                    {
                        ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                    }

                    else
                    {
                        var path = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot\\images",
                       post.ImageUpload.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            post.ImageUpload.CopyTo(stream);
                            path = path.Replace('\\', '/');
                        }

                        int pos = path.LastIndexOf("/") + 1;
                        path = path.Substring(pos, path.Length - pos);

                        post.Categories = Validate.PopulateCategories();
                        if (post.CategoryIds != null)
                        {

                            selectedItems = post.Categories.Where(p => post.CategoryIds.Contains(int.Parse(p.Value))).ToList();

                            ViewBag.Message = "Selected Fruits:";
                         }

                        Post Post = new Post();
                        Post.Title = post.Title;
                        Post.Content = post.Content;
                        Post.Image = path;
                        Post.Created_Time = Post.Created_Time;

                        string username = HttpContext.User.Identities.First(x => x.IsAuthenticated == true).Name;
                        Post.UserRefID = unitOfWork.UserRepository.GetAll().First(x => x.Username == username).User_ID;
                        unitOfWork.Post.Add(Post);

                        unitOfWork.SaveChanges();



                        int last = unitOfWork.Post.GetAll().Last().Post_ID;

                        foreach (int a in post.CategoryIds)
                        {
                            post_Category.Add(new Post_Category { Category_ID = a, Post_ID = last });
                        }

                        unitOfWork.Post_Category.AddRange(post_Category);
                        unitOfWork.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            post.Categories = Validate.PopulateCategories();
            return View(post);
        }


        [Authorize]
        public IActionResult Comment(int? id)
        {
            try
            {
                Post_CategoryViewModel comment = new Post_CategoryViewModel();
               
                var unitOfWork = new UnitOfWork(new EFDataContext());

                Post post = unitOfWork.Post.Get(id);

                comment.Post_ID = post.Post_ID;
                comment.Title = post.Title;
                comment.Content = post.Content;
                comment.Image = post.Image;
                comment.Date = post.Created_Time;
                comment.Username = unitOfWork.UserRepository.GetAll().First(x => x.User_ID == post.UserRefID).Username;

                       
                    return View(comment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Comment(Comment comment, int id)
        {
            Post_CategoryViewModel post = new Post_CategoryViewModel();

         
            using (var unitOfWork = new UnitOfWork(new EFDataContext()))
            {
                
                    
                if (ModelState.IsValid)
                {
                    comment.PostRefID = id;
                    comment.Username = HttpContext.User.Identities.First(x => x.IsAuthenticated == true).Name;
                    comment.Created_Time = DateTime.Now;
                    if (String.IsNullOrWhiteSpace(comment.Content))
                        {
                        ViewBag.Error = "Please,Add a Comment";

                    }
                   else {
                        unitOfWork.Comment.Add(comment);
                        unitOfWork.SaveChanges();
                        
                    }
                    
                    RedirectToAction("Comment");
                }

                Post posts = unitOfWork.Post.Get(id);
                post.Post_ID = posts.Post_ID;
                post.Title = posts.Title;
                post.Content = posts.Content;
                post.Image = posts.Image;
                post.Date = posts.Created_Time;
                post.Username = unitOfWork.UserRepository.GetAll().First(x => x.User_ID == posts.UserRefID).Username;


                return View(post);
            }
           
           
        }


        [HttpGet]
        public JsonResult Comments(int ? id)
        {
           
            using (var unitOfWork = new UnitOfWork(new EFDataContext()))
            {
                var commentlist = unitOfWork.Comment.GetAll().Where(p => p.PostRefID == id).ToList();

                return Json(commentlist);

             }
        }
    }

}
