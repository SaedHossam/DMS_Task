using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        //private IApplicationRepository _application;
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        //public IApplicationRepository Application => _application ??= new ApplicationRepository(_context);

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
