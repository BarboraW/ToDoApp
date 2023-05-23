namespace ToDoAppServer.Models
{
    /// <summary>
    /// Represents a task object.
    /// </summary>
    public record TaskObj
    {
        /// <summary>
        /// Gets or sets the unique identifier of the task.
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
        /// Gets or sets the date of creation of the task.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
    }
}
