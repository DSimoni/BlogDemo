using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model
{
    [Table("Comments")]
    public partial class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Comment_ID { get; set; }

        [Column(TypeName = "varchar(500)")]
        [Required]
        public string Content { get; set; }
        
        public DateTime Created_Time { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Username { get; set; }

        public int PostRefID { get; set; }

        public virtual Post Post { get; set; }

    }


}
