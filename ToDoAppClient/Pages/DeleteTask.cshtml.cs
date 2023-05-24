using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ToDoAppClient.Models;

namespace ToDoAppClient.Pages
{
    /// <summary>
    /// Represents the model for DeleteTask razor page.
    /// </summary>
    public class DeleteTaskModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public TaskModel? Task { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTaskModel"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> instance used for HTTP requests.</param>
        public DeleteTaskModel(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Handles the HTTP GET request for the delete task page.
        /// </summary>
        /// <param name="id">The ID of the task to be deleted.</param>
        /// <returns>The <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44356/tasks/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Task = JsonConvert.DeserializeObject<TaskModel>(json);
            }
            else
            {
                return RedirectToPage("/index");
            }

            return Page();
        }

        /// <summary>
        /// Handles the HTTP POST request for deleting a task.
        /// </summary>
        /// <param name="id">The ID of the task to be deleted.</param>
        /// <returns>The <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:44356/tasks/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return RedirectToPage("/index");
        }
    }
}
