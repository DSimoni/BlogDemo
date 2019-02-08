using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_ID { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Username { get; set; }
        //[Column(TypeName = "varchar(20)")]
        //public string Password { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Salt { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Email { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
