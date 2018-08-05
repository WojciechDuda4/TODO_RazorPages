using Microsoft.EntityFrameworkCore;
using TodoList.DataModels;

namespace TodoList.Data
{
    public class TodoTaskContext : DbContext
    {
        public TodoTaskContext(DbContextOptions<TodoTaskContext> options) : base(options)
        {

        }

        public DbSet<TodoTask> TodoList { get; set; }
    }
}
