namespace ToDoAppServer.Models
{
    /// <summary>
    /// Represents the status of a task.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The task has not started.
        /// </summary>
        NotStarted = 0,

        /// <summary>
        /// The task is in progress.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// The task has been completed.
        /// </summary>
        Completed = 2,
    }
}
