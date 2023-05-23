using ToDoAppServer.Models;

namespace ToDoAppServer.Repositories
{
    /// <summary>
    /// Represents an in-memory implementation of the <see cref="IRepository"/> interface.
    /// </summary>
    public class InMemoryRepository: IRepository
    {
        private List<TaskObj> tasks = new();

        /// <inheritdoc/>
        public IEnumerable<TaskObj> GetAllTasks()
        {
            return tasks;
        }

        /// <inheritdoc/>
        public TaskObj GetTaskById(Guid id) 
        {
            var task = tasks.SingleOrDefault(taskobj => taskobj.ID == id);
            return task;
        }

        /// <inheritdoc/>
        public void CreateTask(TaskObj task) 
        {
            tasks.Add(task);
        }

        /// <inheritdoc/>
        public void UpdateTask(TaskObj task)
        {
            var index  = tasks.FindIndex(existingTask => existingTask.ID == task.ID);
            tasks[index] = task;
        }

        /// <inheritdoc/>
        public void DeleteTask(Guid id) 
        {
            var index = tasks.FindIndex(existingTask => existingTask.ID == id);
            tasks.RemoveAt(index);
        }
    }
}
