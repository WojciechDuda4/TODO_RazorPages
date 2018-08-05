using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.DataModels;
using TodoList.Enums;
using TodoList.ViewModels;

namespace TodoList.Pages.TODO
{
    public class CompletedModel : PageModel
    {
        private readonly TodoTaskContext _context;

        private ICollection<TodoTask> completedTasks;

        public CompletedModel(TodoTaskContext context)
        {
            _context = context;
        }

        public ICollection<CompletedTaskView> completedTasksView { get; set; }

        public bool completedTasksExist => (completedTasksView.Count != 0);

        public async Task OnGetAsync()
        {
            completedTasks = await _context.TodoList
                .Where(a => a.Status == TodoTaskStatus.Completed)
                .ToListAsync();

            completedTasksView = completedTasks
                .Select(a => new CompletedTaskView()
                {
                    Description = a.Description,
                    CompletionStamp = a.CompletionStamp
                })
                .ToList();
        }

    }
}