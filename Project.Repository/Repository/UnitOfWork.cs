using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.Repository.Common.IRepository;
using System;
using System.Threading.Tasks;

namespace Project.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;
        private readonly VehicleDbContext _context;
        public UnitOfWork(VehicleDbContext context)
        {
            _context = context;
        }
        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }


        public void Dispose()

        {
            if (!_disposed && _context != null)
            {
                _disposed = true;
                _context.Dispose();
            }
        }
    }
}

