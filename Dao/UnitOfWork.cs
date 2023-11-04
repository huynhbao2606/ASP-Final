using ASP_Final.Dao.IRepository;
using ASP_Final.Data;
using ASP_Final.Models;

namespace ASP_Final.Dao
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IGenericRepository<Vaccine> _vaccine;
        private  IGenericRepository<Models.Type> _type;
        private  IGenericRepository<VaccineSchedule> _schedule;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }


        IGenericRepository<Vaccine> IUnitOfWork.Vaccine
        {
            get
            {
                if (_vaccine == null)
                {
                    this._vaccine = new GenericRepository<Vaccine>(_context);
                }

                return _vaccine;
            }
        }

        IGenericRepository<Models.Type> IUnitOfWork.Type
        {
            get
            {
                if (_type == null)
                {
                    this._type = new GenericRepository<Models.Type>(_context);
                }

                return _type;
            }
        }


        IGenericRepository<VaccineSchedule> IUnitOfWork.Schedule
        {
            get
            {
                if (_schedule == null)
                {
                    this._schedule = new GenericRepository<VaccineSchedule>(_context);
                }

                return _schedule;
            }
        }


        void IDisposable.Dispose()
        {
            _context.Dispose();
        }

        void IUnitOfWork.Save()
        {
            _context.SaveChanges();
        }
    }
}