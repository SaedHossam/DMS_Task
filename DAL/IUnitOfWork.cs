using DAL.Repositories.Interfaces;

namespace DAL
{
    public interface IUnitOfWork
    {
        //IApplicationRepository Application { get; }
        int SaveChanges();
    }
}
