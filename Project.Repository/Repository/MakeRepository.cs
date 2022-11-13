using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.DAL.Data;
using Project.DAL.Data.Repoaitoty;
using Project.DAL.Data.Repository;
using Project.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repository.Repository
{
    public class MakeRepository : Repository<VehicleMake>, IMakeRepository
    {
        private VehicleDbContext VehicleDbContext
        {
            get { return Context as VehicleDbContext; }
        }
        public MakeRepository(VehicleDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<VehicleMake>> GetAllWithModelAsync()
        {
            return await VehicleDbContext.VehicleMakes.Include(m => m.Models)
                .ToListAsync();
        }

        public async Task<VehicleMake> GetWithModelByIdAsync(int id)
        {
            return await VehicleDbContext.VehicleMakes.Include(a => a.Models)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<VehicleMake>> GetPagedList(RequestParams requestParams)
        {
            return await FindAll().OrderBy(m => m.Name).
                Skip((requestParams.PageNumber - 1) * requestParams.PageSize).Take(requestParams.PageSize).
                ToListAsync();
        }
    }


}

