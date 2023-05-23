using ToDoAppServer.Models;

namespace ToDoAppServer.Dtos
{
    /// <summary>
    /// Represents the data transfer object for a task.
    /// </summary>
    public record TaskObjDto
    {
        /// <summary>
        /// Gets or sets the ID of the task.
        /// </summary>
        public Guid ID { get; set; }

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

        /// <summary>
        /// Gets or sets the created date of the task.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
    }
}
