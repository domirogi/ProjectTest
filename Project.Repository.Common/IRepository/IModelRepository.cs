using Project.Common;
using Project.Model.Models;
using Project.Repository.Common.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Common.Interface
{
    public interface IModelRepository : IGenericRepository<VehicleModel>
    {
        Task<IPagedList<VehicleModel>> GetPage(RequestParams requestParams);

    }
}
