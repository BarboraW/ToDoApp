using ToDoAppServer.Models;

namespace ToDoAppServer.Repositories
{
    /// <summary>
    /// Represents an interface for managing tasks.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        /// <returns>An enumerable collection of tasks.</returns>
        IEnumerable<TaskObj> GetAllTasks();

        /// <summary>
        /// Retrieves a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>The task with the specified ID, or null if not found.</returns>
        TaskObj GetTaskById(Guid id);

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">The task to create.</param>
        void CreateTask(TaskObj task);

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="task">The task to update.</param>
        void UpdateTask(TaskObj task);

        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        void DeleteTask(Guid id);
    }
}
