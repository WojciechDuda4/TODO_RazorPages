using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using TodoList.DataModels;
using TodoList.Helpers;
using TodoList.ViewModels;

namespace TodoList.Pages.TODO
{
    public class CompletedModel : PageModel
    {
        IStringLocalizer<CompletedModel> _stringLocalizer;

        private ICollection<TodoTask> _completedTasks;

        private ApiHelper _apiHelper;

        public ViewDataDictionary PartialViewStaticContent
        {
            get
            {
                return new ViewDataDictionary(ViewData);
            }
        }

        public CompletedModel(IStringLocalizer<CompletedModel> stringLocalizer, ApiHelper apiHelper)
        {
            _stringLocalizer = stringLocalizer;
            _apiHelper = apiHelper;
        }

        public ICollection<CompletedTaskViewModel> CompletedTasks { get; set; }

        public bool CompletedTasksExist => (CompletedTasks.Count != 0);

        public async Task OnGetAsync()
        {
            string url = "https://localhost:44349/api/TodoList?todoTaskStatus=Completed";

            SetViewData();

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    _completedTasks = await response.Content.ReadAsAsync<ICollection<TodoTask>>();
                }
            }

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