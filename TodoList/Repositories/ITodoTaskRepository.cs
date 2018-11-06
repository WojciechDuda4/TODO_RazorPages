using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.DataModels;
using TodoList.Enums;

namespace TodoList.Repositories
{
    public interface ITodoTaskRepository : IRepository<TodoTask>
    {
        Task<List<TodoTask>> GetTasksByStatusAsync(TodoTaskStatus todoTaskStatus);
    }
}
