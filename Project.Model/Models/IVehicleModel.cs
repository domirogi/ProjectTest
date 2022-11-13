using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Models
{
    public interface IVehicleModel
    {
        int Id { get; set; }
        int MakeId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        VehicleMake Make { get; set; }
    }
}
