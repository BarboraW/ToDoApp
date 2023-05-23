using ToDoAppServer.Models;

namespace ToDoAppServer.Dtos
{
    /// <summary>
    /// Represents the data transfer object for updating a task.
    /// </summary>
    public record UpdateTaskObjDto
    {
        /// <summary>
        /// Gets or sets the updated name of the task.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the updated priority of the task.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the updated status of the task.
        /// </summary>
        public Status Status { get; set; }
    }
}
