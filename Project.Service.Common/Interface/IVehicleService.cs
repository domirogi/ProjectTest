using Project.Common;
using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Common.Interface
{
   public interface IVehicleService
    {

        Task<List<VehicleMake>> GetMakesAsync();
        Task<VehicleMake> GetMakeByIdAsync(int id);
        Task<IPagedList<VehicleMake>> GetPagedMake(RequestParams requestParams);
        Task CreateMakeAsync(VehicleMake make);
        Task UpdateMakeAsync(VehicleMake make);
        Task DeleteMakeAsync(VehicleMake make);

        Task<List<VehicleModel>> GetModelsAsync();
        Task<VehicleModel> GetModelByIdAsync(int id);
        Task CreateModelAsync(VehicleModel model);
        Task DeleteModelAsync(VehicleModel model);
        Task UpdateModelAsync(VehicleModel model);
        Task<IPagedList<VehicleModel>> GetPagedModel(RequestParams requestParams);

    }
}
