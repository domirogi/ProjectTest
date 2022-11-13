using Project.Common;
using Project.DAL;
using Project.DAL.Data.Repository;
using Project.Model.Models;
using Project.Repository.Common.IRepository;
using Project.Service.Common.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ModelService(IUnitOfWork unitOfWork, IModelRepository modelRepository)
        {
            _unitOfWork = unitOfWork;
            _modelRepository = modelRepository;
        }

        public async Task<VehicleModel> CreateModel(VehicleModel newModel)
        {
            await _modelRepository.AddAsync(newModel);
            await _unitOfWork.CommitAsync();
            return newModel;
        }

        public async Task DeleteModel(VehicleModel model)
        {
            await _modelRepository.RemoveAsync(model);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<VehicleModel>> GetAllWithMake(RequestParams requestParams)
        {
            return await _modelRepository.GetPagedList(requestParams);
        }

        public async Task<VehicleModel> GetModelById(object id)
        {
            return await _modelRepository
                 .GetByIdAsync(id);
        }

        public async Task<IEnumerable<VehicleModel>> GetModelByMaketId(int makeId)
        {
            return await _modelRepository.GetAllWithMakeByMakeIdAsync(makeId);
        }

        public async Task UpdateModel(int id, VehicleModel modelUpdate)
        {
            await _modelRepository.UpdateAsync(id, modelUpdate);
            await _unitOfWork.CommitAsync();
        }


    }
}
