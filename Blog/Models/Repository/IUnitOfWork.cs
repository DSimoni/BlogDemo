using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
