using System.Threading.Tasks;

namespace Project.Repository.Common.IRepository
{
    public interface IUnitOfWork 
    {
        Task<int> CommitAsync();

    }
}
