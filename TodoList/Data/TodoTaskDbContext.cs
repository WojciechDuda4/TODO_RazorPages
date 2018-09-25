using Microsoft.EntityFrameworkCore;
using TodoList.DataModels;

namespace TodoList.Data
{
    public class TodoTaskDbContext : DbContext
    {
        public TodoTaskDbContext(DbContextOptions<TodoTaskDbContext> options) : base(options)
        {

        }

        public DbSet<TodoTask> TodoList { get; set; }
    }
}
