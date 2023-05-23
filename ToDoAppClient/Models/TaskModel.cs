using System.ComponentModel.DataAnnotations;

namespace ToDoAppClient.Models
{
    /// <summary>
    /// Represents a model for a task.
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the task.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the priority of the task.
        /// </summary>
        [Required(ErrorMessage = "The Priority field is required.")]
        [Range(1,10, ErrorMessage = "Enter a value between 1 and 10")]
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the status of the task.
        /// </summary>
        [Required(ErrorMessage = "The Status field is required.")]
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the date of creation of the task.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
    }
}
