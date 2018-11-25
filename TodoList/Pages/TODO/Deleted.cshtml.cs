using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using TodoList.DataModels;
using TodoList.Enums;
using TodoList.Repositories;
using TodoList.ViewModels;

namespace TodoList.Pages.TODO
{
    public class DeletedModel : PageModel
    {
        private IStringLocalizer<DeletedModel> _stringLocalizer;

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

        public DeletedModel(IUnitOfWork unitOfWork, IStringLocalizer<DeletedModel> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
        }

        public async Task OnGetAsync()
        {
            SetViewData();

            _deletedTasks = await _unitOfWork.TodoTaskRepository.GetTasksByStatusAsync(TodoTaskStatus.Deleted);

            DeletedTasks = _deletedTasks
                .Select(a => new DeletedTaskViewModel()
                {
                    Description = a.Description,
                    WriteStamp = a.DeletionStamp
                })
                .ToList();
        }

        void SetViewData()
        {
            ViewData["Title"] = _stringLocalizer["Title"];
            ViewData["DescriptionColumnTitle"] = _stringLocalizer["DescriptionColumnTitle"];
            ViewData["DateColumnTitle"] = _stringLocalizer["DateColumnTitle"];
            ViewData["EmptyTableLabel"] = _stringLocalizer["EmptyTableLabel"];
        }
    }
}