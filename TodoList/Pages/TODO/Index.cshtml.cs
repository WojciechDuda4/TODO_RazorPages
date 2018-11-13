using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoList.DataModels;
using TodoList.Enums;
using TodoList.Repositories;
using TodoList.ViewModels;

namespace TodoList.Pages.TODO
{
    public class IndexModel : PageModel
    {
        private IUnitOfWork _unitOfWork;

        private IEnumerable<TodoTask> _plannedTasks;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlannedTaskViewModel> PlannedTasks { get; set; }

        public bool PlannedTasksExist => (PlannedTasks.Any());

        [BindProperty]
        public PlannedTaskViewModel PlannedTask { get; set; }

        public async Task OnGetAsync()
        {
            _plannedTasks = await _unitOfWork.TodoTaskRepository.GetTasksByStatusAsync(TodoTaskStatus.Planned);

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

            _unitOfWork.TodoTaskRepository.Add(newTodoTask);
            await _unitOfWork.Complete();

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            TodoTask taskToDelete = await _unitOfWork.TodoTaskRepository.Get(Convert.ToInt32(id));

            if (taskToDelete != null)
            {
                taskToDelete.DeletionStamp = DateTime.Now;
                taskToDelete.Status = TodoTaskStatus.Deleted;
                _unitOfWork.Update(taskToDelete);
                await _unitOfWork.Complete();
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAddAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            TodoTask completedTask = await _unitOfWork.TodoTaskRepository.Get(Convert.ToInt32(id));

            if (completedTask != null)
            {
                completedTask.CompletionStamp = DateTime.Now;
                completedTask.Status = TodoTaskStatus.Completed;
                _unitOfWork.Update(completedTask);
                await _unitOfWork.Complete();
            }

            return RedirectToPage("Index");
        }
    }
}