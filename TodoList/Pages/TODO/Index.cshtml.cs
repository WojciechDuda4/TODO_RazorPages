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

        [BindProperty]
        public PlannedTaskViewModel PlannedTask { get; set; }

        public async Task OnGetAsync()
        {
            string url = "https://localhost:44349/api/TodoList?todoTaskStatus=Planned";

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
            string url = "https://localhost:44349/api/TodoList";

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

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync<TodoTask>(url, newTodoTask))
            {
                if (response.IsSuccessStatusCode)
                {
                    // WTF?
                }
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            string url = $"https://localhost:44349/api/TodoList/{id}";

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
                url = "https://localhost:44349/api/TodoList";

                taskToDelete.DeletionStamp = DateTime.Now;
                taskToDelete.Status = TodoTaskStatus.Deleted;

                using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync(url, taskToDelete))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // WTF?
                    }
                }
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAddAsync(int? id)
        {
            string url = $"https://localhost:44349/api/TodoList/{id}";

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

                url = $"https://localhost:44349/api/TodoList";

                using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync(url, completedTask))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // WTF?
                    }
                }
            }

            return RedirectToPage("Index");
        }
    }
}