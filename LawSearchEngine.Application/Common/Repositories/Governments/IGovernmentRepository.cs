using LawSearchEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.Repositories.Agencies
{
    public interface IGovernmentRepository
    {
        public void CreateGovernment(Government government);
        public void UpdateGovernment(Government government);
        public Task<Government> DeleteGovernmentAsync(Guid id);
        public Task<Government?> GetGovernmentByIdAsync(Guid id);
        public Task<Government?> GetGovernmentByUserIdAsync(Guid userId);

    }
}
