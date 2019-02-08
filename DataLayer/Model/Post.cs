using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model
{
    [Table("Posts")]
    public partial class Post
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Post_ID { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Title { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        private DateTime? CreatedDate;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created_Time
        {
            get { return CreatedDate ?? DateTime.UtcNow; }
            set { CreatedDate = value; }
        }

           public DateTime Update_Time { get; set; }

        public int UserRefID { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Post_Category> Post_Categories { get; set; }

    }

    [Table("Category")]
    public partial class Category
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_ID { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }

        public virtual ICollection<Post_Category> Post_Categories { get; set; }



    }
}
