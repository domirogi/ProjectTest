using Project.Common;
using Project.Model.Models;
using Project.Service.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Service
{
   public class VehicleService : IVehicleService
    {
        private readonly IMakeRepository _makeRepository;
        private readonly IModelRepository _modelRepository;

        public VehicleService(IMakeRepository makeRepository, IModelRepository modelRepository)
        {
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
        }

        public Task CreateMakeAsync(VehicleMake make)
        {
            return _makeRepository.CreateAsync(make);
        }

        public Task CreateModelAsync(VehicleModel model)
        {
            return _modelRepository.CreateAsync(model);
        }

        public Task DeleteMakeAsync(VehicleMake make)
        {
            return _makeRepository.DeleteAsync(make);
        }

        public Task DeleteModelAsync(VehicleModel model)
        {
            return _modelRepository.DeleteAsync(model);
        }

        public Task<VehicleMake> GetMakeByIdAsync(int id)
        {
            return _makeRepository.Get(m=>m.Id==id);
        }

        public async Task<List<VehicleMake>> GetMakesAsync()
        {
            return await _makeRepository.GetAllAsync();
        }

        public async Task<VehicleModel> GetModelByIdAsync(int id)
        {
            return await _modelRepository.Get(m => m.Id == id);
        }

        public async Task<List<VehicleModel>> GetModelsAsync()
        {
            return await _modelRepository.GetAllAsync();
        }

        public Task<IPagedList<VehicleMake>> GetPagedMake(RequestParams requestParams)
        {
            return _makeRepository.GetPage(requestParams);
        }

        public Task<IPagedList<VehicleModel>> GetPagedModel(RequestParams requestParams)
        {
            return _modelRepository.GetPage(requestParams);
        }

        public Task UpdateMakeAsync(VehicleMake make)
        {
            return _makeRepository.UpdateAsync(make);
        }

        public Task UpdateModelAsync(VehicleModel model)
        {
            return _modelRepository.UpdateAsync(model);
        }
    }
}
