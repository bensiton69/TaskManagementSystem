using System.Threading.Tasks;

namespace TaskManagementSystem.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}