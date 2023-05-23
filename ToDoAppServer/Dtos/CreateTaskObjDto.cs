using ToDoAppServer.Models;

namespace ToDoAppServer.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a task.
    /// </summary>
    public record CreateTaskObjDto
    {
        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the priority of the task.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the status of the task.
        /// </summary>
        public Status Status { get; set; }
    }
}
