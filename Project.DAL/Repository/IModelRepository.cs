using Project.Common;
using Project.Model.Models;
using Project.Repository.Common.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.DAL.Data.Repository
{
    public interface IModelRepository : IRepository<VehicleModel>
    {
        Task<IEnumerable<VehicleModel>> GetAllWithMakeAsync();
        Task<VehicleModel> GetWithMakeByIdAsync(int id);
        Task<IEnumerable<VehicleModel>> GetAllWithMakeByMakeIdAsync(int makeId);

        Task<IEnumerable<VehicleModel>> GetPagedList(RequestParams requestParams);
    }
}
