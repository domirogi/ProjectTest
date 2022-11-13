using Project.Common;
using Project.Model.Models;
using Project.Repository.Common.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.DAL.Data.Repoaitoty
{
    public interface IMakeRepository : IRepository<VehicleMake>
    {
        Task<IEnumerable<VehicleMake>> GetAllWithModelAsync();
        Task<VehicleMake> GetWithModelByIdAsync(int id);
        Task<IEnumerable<VehicleMake>> GetPagedList(RequestParams requestParams);
    }
}
