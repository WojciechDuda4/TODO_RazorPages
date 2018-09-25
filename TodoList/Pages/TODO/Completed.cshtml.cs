using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.DataModels;
using TodoList.Enums;
using TodoList.ViewModels;

namespace TodoList.Pages.TODO
{
    public class CompletedModel : PageModel
    {
        private readonly TodoTaskDbContext _context;

        private ICollection<TodoTask> _completedTasks;

        public ViewDataDictionary PartialViewStaticContent
        {
            get
            {
                return new ViewDataDictionary(ViewData);
            }
        }

        public CompletedModel(TodoTaskDbContext context)
        {
            _context = context;
        }

        public ICollection<CompletedTaskViewModel> CompletedTasks { get; set; }

        public bool CompletedTasksExist => (CompletedTasks.Count != 0);

        public async Task OnGetAsync()
        {
            SetViewData();

            _completedTasks = await _context.TodoList
                .Where(a => a.Status == TodoTaskStatus.Completed)
                .ToListAsync();

            CompletedTasks = _completedTasks
                .Select(a => new CompletedTaskViewModel()
                {
                    Description = a.Description,
                    WriteStamp = a.CompletionStamp
                })
                .ToList();
        }

        void SetViewData()
        {
            ViewData["Title"] = "Completed tasks";
            ViewData["DescriptionColumnTitle"] = "Description";
            ViewData["DateColumnTitle"] = "Completion Date";
            ViewData["EmptyTableLabel"] = "No tasks completed";
        }
    }
}