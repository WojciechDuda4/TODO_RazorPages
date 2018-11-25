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
    public class CompletedModel : PageModel
    {
        private IUnitOfWork _unitOfWork;

        IStringLocalizer<CompletedModel> _stringLocalizer;

        private ICollection<TodoTask> _completedTasks;

        public ViewDataDictionary PartialViewStaticContent
        {
            get
            {
                return new ViewDataDictionary(ViewData);
            }
        }

        public CompletedModel(IUnitOfWork unitOfWork, IStringLocalizer<CompletedModel> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
        }

        public ICollection<CompletedTaskViewModel> CompletedTasks { get; set; }

        public bool CompletedTasksExist => (CompletedTasks.Count != 0);

        public async Task OnGetAsync()
        {
            SetViewData();

            _completedTasks = await _unitOfWork.TodoTaskRepository.GetTasksByStatusAsync(TodoTaskStatus.Completed);

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
            ViewData["Title"] = _stringLocalizer["Title"];
            ViewData["DescriptionColumnTitle"] = _stringLocalizer["DescriptionColumnTitle"];
            ViewData["DateColumnTitle"] = _stringLocalizer["DateColumnTitle"];
            ViewData["EmptyTableLabel"] = _stringLocalizer["EmptyTableLabel"];
        }
    }
}