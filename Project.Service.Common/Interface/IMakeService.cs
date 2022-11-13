using Project.Common;
using Project.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Common.Interface
{
    public interface IMakeService
    {
        Task<IEnumerable<VehicleMake>> GetAllMake(RequestParams requestParams);
        Task<VehicleMake> GetMakeById(object id);
        Task<VehicleMake> CreateMake(VehicleMake newMake);
        Task UpdateMake(int id,VehicleMake makeUpdate);
        Task DeleteMake(VehicleMake make);
    }
}
