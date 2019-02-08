using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{

    public class EFDataContext : DbContext
    {
      

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post_Category> Post_Categories { get; set; }

    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DHIMO-SIMONI\SQLEXPRESS;Initial Catalog=Blog;User ID=sa;password=streetfighter12");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
           .HasOne(s => s.User)
           .WithMany(g => g.Posts)
           .HasForeignKey(l => l.UserRefID); //one to many

            modelBuilder.Entity<Comment>()
          .HasOne(s => s.Post)
          .WithMany(g => g.Comments)
          .HasForeignKey(l => l.PostRefID);


            modelBuilder.Entity<Post_Category>()
           .HasKey(pc => new { pc.Post_ID, pc.Category_ID });

            modelBuilder.Entity<Post_Category>()
                .HasOne(pc => pc.Post)
                .WithMany(p => p.Post_Categories)
                .HasForeignKey(pc => pc.Post_ID);

            modelBuilder.Entity<Post_Category>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.Post_Categories)
                .HasForeignKey(pc => pc.Category_ID);
        }
    }
}
