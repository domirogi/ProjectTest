using Project.Common;
using Project.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Common.Interface
{
    public interface IModelService
    {
        Task<IEnumerable<VehicleModel>> GetAllWithMake(RequestParams requestParams);
        Task<VehicleModel> GetModelById(object id);
        Task<IEnumerable<VehicleModel>> GetModelByMaketId(int makeId);
        Task<VehicleModel> CreateModel(VehicleModel newModel);
        Task UpdateModel(int id, VehicleModel modelUpdate);
        Task DeleteModel(VehicleModel model);
    }
}
