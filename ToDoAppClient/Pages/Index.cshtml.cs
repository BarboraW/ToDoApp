using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using ToDoAppClient.Models;
using ToDoAppClient.Wrappers;

namespace ToDoAppClient.Pages
{
    /// <summary>
    /// Represents the model for Index razor page that displays list of tasks.
    /// </summary>
    public class TasksListModel : PageModel
    {
        private readonly IHttpClientWrapper _httpClient;

        public List<TaskModel>? Tasks { get; set; }

        [BindProperty]
        public TaskModel? NewTask { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TasksListModel"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> instance used for HTTP requests.</param>
        public TasksListModel(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Handles the HTTP GET request for the index page.
        /// </summary>
        /// <returns>The <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnGet()
        {
            await LoadTasksAsync();
            return Page();
        }

        /// <summary>
        /// Handles the HTTP POST request for creating a new task.
        /// </summary>
        /// <returns>The <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostAsync()
         {
            if (ModelState.IsValid)
            {
                try
                {
                    var task = NewTask;
                    var response = await _httpClient.PostAsJsonAsync("https://localhost:44356/tasks", NewTask);

                    if (response.IsSuccessStatusCode)
                    {
                        await LoadTasksAsync();
                        return Page();
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, errorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the task.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while communicating with the server.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred.");
                }
            }

            await LoadTasksAsync();
            return Page();
        }

        /// <summary>
        /// Loads the tasks asynchronously from the server API.
        /// </summary>
        /// <returns>The <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task LoadTasksAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44356/tasks");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Tasks = JsonConvert.DeserializeObject<List<TaskModel>>(json);
        }
    }
}
