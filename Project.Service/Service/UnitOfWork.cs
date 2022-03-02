using Project.DAL.Data;
using Project.Repository.Common.IRepository;
using Project.Service.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly VehicleDbContext _context;

       
        public IMakeRepository Make { get; private set; }
        public IModelRepository Model { get; private set; }
        public UnitOfWork(VehicleDbContext context)
        {
            _context = context;
            Make = new MakeRepository(_context);
            Model = new ModelRepository(_context);

        }
                
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task SaveAsync() => _context.SaveChangesAsync();

    }
}

