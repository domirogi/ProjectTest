using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Model.Models;
using Project.DAL.Data;
using Project.DAL.Data.Repository;
using Project.Common;

namespace Project.Repository.Repository
{ 
    public class ModelRepository : Repository<VehicleModel>, IModelRepository
     {
    private VehicleDbContext VehicleDbContext
    {
        get { return Context as VehicleDbContext; }
    }
    public ModelRepository(VehicleDbContext context)
        : base(context)
    { }

    public async Task<IEnumerable<VehicleModel>> GetAllWithMakeAsync()
    {
        return await VehicleDbContext.VehicleModels.Include(m => m.Make).ToListAsync();
    }

    public async Task<VehicleModel> GetWithMakeByIdAsync(int id)
    {
        return await VehicleDbContext.VehicleModels.Include(m => m.Make)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<VehicleModel>> GetAllWithMakeByMakeIdAsync(int makeId)
    {
        return await VehicleDbContext.VehicleModels.Include(m => m.Make)
            .Where(m => m.MakeId == makeId)
            .ToListAsync();
    }
    public async Task<IEnumerable<VehicleModel>> GetPagedList(RequestParams requestParams)
    {
        return await FindAll().Include(m => m.Make).
            Skip((requestParams.PageNumber - 1) * requestParams.PageSize).Take(requestParams.PageSize).
            ToListAsync();
    }
}

}



