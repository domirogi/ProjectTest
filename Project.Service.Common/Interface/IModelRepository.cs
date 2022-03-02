using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common.Interface
{
    public interface IModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetAllAsync(bool trackChanges);
        Task<IEnumerable<VehicleModel>> GetModelsAsync(int makeId, bool trackChanges);
       Task<VehicleModel> GetModelAsync(int makeId, int id, bool trackChanges);
        Task<IEnumerable<VehicleModel>> GetAsync(bool trackChanges);
        void CreateModel(int makeId, VehicleModel model);
        void DeleteModel(VehicleModel model);


    }
}
