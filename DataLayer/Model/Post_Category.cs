using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Model
{
    [Table("Post_Category")]
    public class Post_Category
    {
        [Key]
        [Column(Order = 1)]
        public int Post_ID { get; set; }
        public Post Post { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Category_ID { get; set; }
        public Category Category { get; set; }
    }
}
