using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Models
{
   public class VehicleModel : IVehicleModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        [ForeignKey("MakeId")]
        public int MakeId { get; set; }
        public virtual VehicleMake Make { get; set; }
    }
}
