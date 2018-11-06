using System.Threading.Tasks;
using TodoList.DataModels;

namespace TodoList.Repositories
{
    public interface IUnitOfWork
    {
        ITodoTaskRepository TodoTaskRepository { get; }
        Task<int> Complete();
        void Update(TodoTask updatedTodoTask);
    }
}
