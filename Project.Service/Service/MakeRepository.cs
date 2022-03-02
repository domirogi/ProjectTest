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
 public class MakeRepository : GenericRepository<VehicleMake>, IMakeRepository
    {
        public MakeRepository(VehicleDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<VehicleMake>> GetAllMakesAsync(bool trackChanges) =>
          await FindAll(trackChanges).OrderBy(m => m.Name).ToListAsync();

        public async Task<VehicleMake> GetMakeAsync(int makeId, bool trackChanges) =>
           await FindByCondition(m => m.Id.Equals(makeId), trackChanges).SingleOrDefaultAsync();
        public void CreateMake(VehicleMake make) => Create(make);
        

        public void DeleteMake(VehicleMake make)
        {
            Delete(make);
        }

       
    }
}
