using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using TodoList.DataModels;
using TodoList.Enums;
using TodoList.Helpers;
using TodoList.ViewModels;

namespace TodoList.Pages.TODO
{
    public class IndexModel : PageModel
    {
        IStringLocalizer<IndexModel> _stringLocalizer;

        private IEnumerable<TodoTask> _plannedTasks;

        private ApiHelper _apiHelper;

        public IndexModel(IStringLocalizer<IndexModel> stringLocalizer, ApiHelper apiHelper)
        {
            _stringLocalizer = stringLocalizer;
            _apiHelper = apiHelper;
        }

        public IEnumerable<PlannedTaskViewModel> PlannedTasks { get; set; }

        public bool PlannedTasksExist => (PlannedTasks.Any());

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        [BindProperty]
        public PlannedTaskViewModel PlannedTask { get; set; }

        public async Task OnGetAsync()
        {
            string url = "api/TodoTasks?todoTaskStatus=Planned";

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    _plannedTasks = await response.Content.ReadAsAsync<IEnumerable<TodoTask>>();
                }
            }

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
            string url = "api/TodoTasks";

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

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync(url, newTodoTask))
            {
                if (response.IsSuccessStatusCode)
                {
                    Message = _stringLocalizer["TaskCreatedMessage"];
                }
                else
                {
                    Message = _stringLocalizer["TaskCreatingErrorMessage"];
                }
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            string url = $"api/TodoTasks/{id}";

            if (id == null)
            {
                return Page();
            }

            TodoTask taskToDelete = null;

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    taskToDelete = await response.Content.ReadAsAsync<TodoTask>();
                }
            }

            if (taskToDelete != null)
            {
                url = "api/TodoTasks";

                taskToDelete.DeletionStamp = DateTime.Now;
                taskToDelete.Status = TodoTaskStatus.Deleted;

                using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync(url, taskToDelete))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Message = _stringLocalizer["TaskDeletedMessage"];
                    }
                    else
                    {
                        Message = _stringLocalizer["TaskDeletingErrorMessage"];
                    }
                }
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAddAsync(int? id)
        {
            string url = $"api/TodoTasks/{id}";

            if (id == null)
            {
                return Page();
            }

            TodoTask completedTask = null;

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    completedTask = await response.Content.ReadAsAsync<TodoTask>();
                }
            }

            if (completedTask != null)
            {
                completedTask.CompletionStamp = DateTime.Now;
                completedTask.Status = TodoTaskStatus.Completed;

                url = $"api/TodoTasks";

                using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync(url, completedTask))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Message = _stringLocalizer["TaskCompletedMessage"];
                    }
                    else
                    {
                        Message = _stringLocalizer["TaskCompletingErrorMessage"];
                    }
                }
            }

            return RedirectToPage("Index");
        }
    }
}