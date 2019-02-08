using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Model;

namespace Blog.Models.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
       
        private readonly EFDataContext db;
        private Repository<User> userRepository;
        private Repository<Category> categoryRepository;
        private Repository<Post> postRepository;
        private Repository<Post_Category> post_categoryRepository;
        private Repository<Comment> commentRepository;


        public UnitOfWork(EFDataContext context)
        {
            db = context;
            
        }

        public Repository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new Repository<User>(db);
                }
                return userRepository;
            }
        }

        public Repository<Category> Categories
        {
            get
            {

                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new Repository<Category>(db);
                }
                return categoryRepository;
            }
        }

        public Repository<Post> Post
        {
            get
            {

                if (this.postRepository == null)
                {
                    this.postRepository = new Repository<Post>(db);
                }
                return postRepository;
            }
        }

        public Repository<Post_Category> Post_Category
        {
            get
            {

                if (this.post_categoryRepository == null)
                {
                    this.post_categoryRepository = new Repository<Post_Category>(db);
                }
                return post_categoryRepository;
            }
        }

        public Repository<Comment> Comment
        {
            get
            {

                if (this.commentRepository == null)
                {
                    this.commentRepository = new Repository<Comment>(db);
                }
                return commentRepository;
            }
        }


        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
