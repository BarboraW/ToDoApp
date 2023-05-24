using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using ToDoAppClient.Models;

namespace ToDoAppClient.Pages
{
    /// <summary>
    /// Represents the model for UpdateTask razor page.
    /// </summary>
    public class UpdateTaskModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public TaskModel? UpdatedTask { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTaskModel"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> instance used for HTTP requests.</param>
        public UpdateTaskModel(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Handles the HTTP GET request for the update task page.
        /// </summary>
        /// <param name="id">The ID of the task to be updated.</param>
        /// <returns>The <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44356/tasks/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            UpdatedTask = JsonConvert.DeserializeObject<TaskModel>(json);

            return Page();
        }

        /// <summary>
        /// Handles the HTTP POST request for updating a task.
        /// </summary>
        /// <param name="id">The ID of the task to be updated.</param>
        /// <returns>The <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var task = UpdatedTask;
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44356/tasks/{task.Id}", task);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Task not found.");
                    return Page();
                }

                return RedirectToPage("/index");
            }

            return Page();
        }
    }
}


