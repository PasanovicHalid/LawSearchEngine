using LawSearchEngine.Application.Common.Repositories;
using LawSearchEngine.Persistance.Context;

namespace LawSearchEngine.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LawSearchEngineDbContext _db;

        public UnitOfWork(LawSearchEngineDbContext db)
        {
            _db = db;
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
