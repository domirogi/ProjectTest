using Microsoft.EntityFrameworkCore;
using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Data
{
  public class VehicleDbContext : DbContext
    {
        public VehicleDbContext()
        {
        }

        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {

        }
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        
    }
}
