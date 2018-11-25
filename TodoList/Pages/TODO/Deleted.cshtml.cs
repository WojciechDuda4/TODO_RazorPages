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
    public class DeletedModel : PageModel
    {
        private IStringLocalizer<DeletedModel> _stringLocalizer;

        private ICollection<TodoTask> _deletedTasks;

        private ApiHelper _apiHelper;

        public ViewDataDictionary PartialViewStaticContent
        {
            get
            {
                return new ViewDataDictionary(ViewData);
            }
        }

        public ICollection<DeletedTaskViewModel> DeletedTasks { get; set; }

        public bool DeletedTasksExist => (DeletedTasks.Count != 0);

        public DeletedModel(IStringLocalizer<DeletedModel> stringLocalizer, ApiHelper apiHelper)
        {
            _stringLocalizer = stringLocalizer;
            _apiHelper = apiHelper;
        }

        public async Task OnGetAsync()
        {
            string url = "https://localhost:44349/api/TodoList?todoTaskStatus=Deleted";

           SetViewData();

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    _deletedTasks = await response.Content.ReadAsAsync<ICollection<TodoTask>>();
                }
            }

            DeletedTasks = _deletedTasks
                .Select(a => new DeletedTaskViewModel()
                {
                    Description = a.Description,
                    WriteStamp = a.WriteStamp
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