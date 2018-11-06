using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.DataModels;
using TodoList.Enums;

namespace TodoList.Repositories
{
    public class TodoTaskRepository : Repository<TodoTask>, ITodoTaskRepository
    {
        private TodoTaskDbContext todoTaskDbContext
        {
            get { return dbContext as TodoTaskDbContext; }
        }

        public TodoTaskRepository(TodoTaskDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TodoTask>> GetTasksByStatusAsync(TodoTaskStatus todoTaskStatus)
        {
            return await Find(a => a.Status == todoTaskStatus);
        }
    }
}
