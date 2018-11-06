using System.Threading.Tasks;
using TodoList.Data;
using TodoList.DataModels;

namespace TodoList.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoTaskDbContext _dbContext;

        public ITodoTaskRepository TodoTaskRepository { get; private set; }

        public UnitOfWork(TodoTaskDbContext dbContext)
        {
            _dbContext = dbContext;
            TodoTaskRepository = new TodoTaskRepository(_dbContext);
        }

        public Task<int> Complete()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Update(TodoTask updatedTodoTask)
        {
            _dbContext.TodoList.Update(updatedTodoTask);
        }
    }
}
