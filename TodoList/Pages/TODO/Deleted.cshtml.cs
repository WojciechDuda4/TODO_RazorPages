using System;
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
    public class DeletedModel : PageModel
    {
        private readonly TodoTaskContext _context;

        private ICollection<TodoTask> deletedTasks;

        public ICollection<DeletedTaskView> deletedTasksView { get; set; }

        public bool deletedTasksExist => (deletedTasksView.Count != 0);

        public DeletedModel(TodoTaskContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            deletedTasks = await _context.TodoList
                .Where(a => a.Status == TodoTaskStatus.Deleted)
                .ToListAsync();

            deletedTasksView = deletedTasks
                .Select(a => new DeletedTaskView()
                {
                    Description = a.Description,
                    DeletionStamp = DateTime.Now
                })
                .ToList();
        }
    }
}