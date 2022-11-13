using Project.Common;
using Project.DAL.Data.Repoaitoty;
using Project.DAL.Data.Repository;
using Project.Model.Models;
using Project.Repository.Common.IRepository;
using Project.Service.Common.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public class MakeService : IMakeService
    {
        private readonly IMakeRepository _makeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MakeService(IUnitOfWork unitOfWork, IMakeRepository makeRepository)
        {
            _unitOfWork = unitOfWork;
            _makeRepository = makeRepository;
        }

        public async Task<VehicleMake> CreateMake(VehicleMake newMake)
        {
            await _makeRepository.AddAsync(newMake);
            await _unitOfWork.CommitAsync();
            return newMake;
        }

        public async Task DeleteMake(VehicleMake make)
        {
            await _makeRepository.RemoveAsync(make);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<VehicleMake>> GetAllMake(RequestParams requestParams)
        {
            return await _makeRepository.GetPagedList(requestParams);
        }

        public async Task<VehicleMake> GetMakeById(object id)
        {
            return await _makeRepository.GetByIdAsync(id);
        }

        public async Task UpdateMake(int id, VehicleMake makeUpdate)
        {
            await _makeRepository.UpdateAsync(id, makeUpdate);
            await _unitOfWork.CommitAsync();

        }
    }
}
