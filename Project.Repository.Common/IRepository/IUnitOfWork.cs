using Project.Model.Models;
using Project.Service.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IMakeRepository Make { get; }
        IModelRepository Model { get; }
        Task SaveAsync();
    }
}
