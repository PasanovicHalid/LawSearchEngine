using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
