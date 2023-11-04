using ASP_Final.Models;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace ASP_Final.Dao.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IGenericRepository<Vaccine> Vaccine { get; }

        IGenericRepository<Models.Type> Type { get; }

        IGenericRepository<VaccineSchedule> Schedule { get; }


    }
}