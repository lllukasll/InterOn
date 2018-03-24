using System.Threading.Tasks;

namespace InterOn.Repo.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}