using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.Model.Models;
using Project.Repository.Repository;
using Project.Service.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
  public  class ModelRepository : GenericRepository<VehicleModel>, IModelRepository
    {
        public ModelRepository(VehicleDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<VehicleModel>> GetAsync(bool trackChanges) =>
           await FindAll(trackChanges).OrderBy(m => m.Name).ToListAsync();

        public async Task<VehicleModel> GetModelAsync(int makeId, int id, bool trackChanges) =>
           await FindByCondition(m => m.MakeId.Equals(makeId) && m.Id.Equals(id),
                trackChanges).SingleOrDefaultAsync();


        public async Task<IEnumerable<VehicleModel>> GetModelsAsync(int makeId, bool trackChanges) =>
          await FindByCondition(m => m.MakeId.Equals(makeId), trackChanges)
            .OrderBy(m => m.Name).ToListAsync();
        public void CreateModel(int makeId, VehicleModel model)
        {
            model.MakeId = makeId;
        }

        public void DeleteModel(VehicleModel model)
        {
            Delete(model);
        }

        public async Task<IEnumerable<VehicleModel>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(m => m.Name).ToListAsync();
        }
    }
}
