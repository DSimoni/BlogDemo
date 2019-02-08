
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Blog.Models.ViewModel
{
    public partial class PostViewModel
    {


        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Content cannot be longer than 50 characters and less than 10 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(3000, MinimumLength = 100, ErrorMessage = "Content cannot be longer than 3000 characters and less than 100 characters")]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile ImageUpload { get; set; }


        public List<SelectListItem> Categories { get; set; }

        [Required]
        public int[] CategoryIds { get; set; }






    }
}
