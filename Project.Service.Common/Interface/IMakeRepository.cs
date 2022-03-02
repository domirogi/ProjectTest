using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common.Interface
{
    public interface IMakeRepository
    {
        Task<IEnumerable<VehicleMake>> GetAllMakesAsync(bool trackChanges);
        Task<VehicleMake> GetMakeAsync(int makeId, bool trackChanges);
        void DeleteMake(VehicleMake make);
        void CreateMake(VehicleMake make);
    }
}
