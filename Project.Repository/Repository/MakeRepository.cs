using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.DAL.Data;
using Project.Model.Models;
using Project.Repository.Repository;
using Project.Service.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository.Repository
{
 public class MakeRepository : GenericRepository<VehicleMake>, IMakeRepository
    {
        public MakeRepository(VehicleDbContext context) : base(context)
        {

        }

        
        public Task<IPagedList<VehicleMake>> GetPage(RequestParams requestParams)
        {
            return GetPagedList(requestParams);
        }
    }
}
