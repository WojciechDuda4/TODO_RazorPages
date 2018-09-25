using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.DataModels;
using TodoList.Enums;
using TodoList.ViewModels;

namespace TodoList.Pages.TODO
{
    public class IndexModel : PageModel
    {
        private readonly TodoTaskDbContext _context;

        private ICollection<TodoTask> _plannedTasks;

        public IndexModel(TodoTaskDbContext context)
        {
            _context = context;
        }

        public ICollection<PlannedTaskViewModel> PlannedTasks { get; set; }

        public bool PlannedTasksExist => (PlannedTasks.Count != 0);

        [BindProperty]
        public PlannedTaskViewModel PlannedTask { get; set; }

        public async Task OnGetAsync()
        {
            _plannedTasks = await _context.TodoList
                .Where(a => a.Status == TodoTaskStatus.Planned)
                .ToListAsync();

            PlannedTasks = _plannedTasks
                .Select(a => new PlannedTaskViewModel()
                {
                    Id = a.Id,
                    Description = a.Description,
                    WriteStamp = a.WriteStamp
                })
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TodoTask newTodoTask = new TodoTask
            {
                Description = PlannedTask.Description,
                WriteStamp = DateTime.Now,
                Status = TodoTaskStatus.Planned
            };

            _context.TodoList.Add(newTodoTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            TodoTask taskToDelete = await _context.TodoList.FindAsync(id);

            if (taskToDelete != null)
            {
                taskToDelete.DeletionStamp = DateTime.Now;
                taskToDelete.Status = TodoTaskStatus.Deleted;
                _context.TodoList.Update(taskToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAddAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            TodoTask completedTask = await _context.TodoList.FindAsync(id);

            if (completedTask != null)
            {
                completedTask.CompletionStamp = DateTime.Now;
                completedTask.Status = TodoTaskStatus.Completed;
                _context.TodoList.Update(completedTask);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}