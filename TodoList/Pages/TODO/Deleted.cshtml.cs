using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TodoList.Data;
using TodoList.DataModels;
using TodoList.Enums;
using TodoList.Repositories;
using TodoList.ViewModels;

namespace TodoList.Pages.TODO
{
    public class DeletedModel : PageModel
    {
        private IUnitOfWork _unitOfWork;

        private ICollection<TodoTask> _deletedTasks;

        public ViewDataDictionary PartialViewStaticContent
        {
            get
            {
                return new ViewDataDictionary(ViewData);
            }
        }

        public ICollection<DeletedTaskViewModel> DeletedTasks { get; set; }

        public bool DeletedTasksExist => (DeletedTasks.Count != 0);

        public DeletedModel(TodoTaskDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public async Task OnGetAsync()
        {
            SetViewData();

            _deletedTasks = await _unitOfWork.TodoTaskRepository.GetTasksByStatusAsync(TodoTaskStatus.Deleted);

            DeletedTasks = _deletedTasks
                .Select(a => new DeletedTaskViewModel()
                {
                    Description = a.Description,
                    WriteStamp = DateTime.Now
                })
                .ToList();
        }

        void SetViewData()
        {
            ViewData["Title"] = "Deleted tasks";
            ViewData["DescriptionColumnTitle"] = "Description";
            ViewData["DateColumnTitle"] = "Deletion Date";
            ViewData["EmptyTableLabel"] = "No tasks deleted";
        }
    }
}