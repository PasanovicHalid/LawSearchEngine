using LawSearchEngine.Application.Common.Repositories.Agencies;
using LawSearchEngine.Domain.Entities;
using LawSearchEngine.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace LawSearchEngine.Persistance.Repositories.Agencies
{
    public class GovernmentRepository : IGovernmentRepository
    {
        private readonly DbSet<Government> _governments;

        public GovernmentRepository(LawSearchEngineDbContext context)
        {
            _governments = context.Governments;
        }

        public void CreateGovernment(Government government)
        {
            _governments.Add(government);
        }

        public Task<Government> DeleteGovernmentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Government?> GetGovernmentByIdAsync(Guid id)
        {
            return _governments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateGovernment(Government government)
        {
            _governments.Update(government);
        }

        public Task<Government?> GetGovernmentByUserIdAsync(Guid id)
        {
            return _governments.FirstOrDefaultAsync(x => x.UserId == id);
        }
    }
}
