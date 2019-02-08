using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Model;

namespace Blog.Models.ViewModel
{
    public class Post_CategoryViewModel
    {

        public int Post_ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

         public string Image { get; set; }

        public DateTime Date { get; set;  }

        public string Username { get; set; }

        [Required]
        public Comment Comment { get; set; }
    }
}
