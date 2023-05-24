using Microsoft.AspNetCore.Mvc;
using ToDoAppServer.Dtos;
using ToDoAppServer.Extensions;
using ToDoAppServer.Models;
using ToDoAppServer.Repositories;

namespace ToDoAppServer.Controllers
{
    /// <summary>
    /// API controller for managing tasks.
    /// </summary>
    [ApiController]
    [Route("tasks")]
    public class TasksController : Controller
    {
        private readonly IRepository collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TasksController"/> class.
        /// </summary>
        /// <param name="collection">The task repository.</param>
        public TasksController(IRepository collection) 
        {
            this.collection = collection;
        }

        /// <summary>
        /// Gets all tasks.
        /// </summary>
        /// <returns>An enumeration of task objects.</returns>
        [HttpGet]
        public IEnumerable<TaskObjDto> GetTasks()
        {
            var tasks = collection.GetAllTasks().Select(taskObj => taskObj.AsDto());
            return tasks;
        }

        /// <summary>
        /// Gets a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <returns>The task object.</returns>
        [HttpGet("{id}")]
        public ActionResult<TaskObjDto> GetTaskById(Guid id)
        {
            try
            {
                var task = collection.GetTaskById(id);
                return task.AsDto();
            }

            catch (InvalidOperationException ex)
            {
                return NotFound();
            }

            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="taskDto">The task DTO.</param>
        /// <returns>The created task.</returns>
        [HttpPost]
        public IActionResult CreateTask(CreateTaskObjDto taskDto)
        {
            var tasks = collection.GetAllTasks();

            // Check if a task with the same name already exists
            if (tasks.Any(t => t.Name == taskDto.Name))
            {
                return BadRequest("A task with the same name already exists.");
            }

            TaskObj task = new()
            {
                ID = Guid.NewGuid(),
                Name = taskDto.Name,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            collection.CreateTask(task);
            return Ok(task);
        }

        /// <summary>
        /// Updates a task.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="taskDto">The updated task DTO.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateTask(Guid id, UpdateTaskObjDto taskDto)
        {
            try
            {
                var existingTask = collection.GetTaskById(id);
                var updatedTask = existingTask with
                {
                    Name = taskDto.Name,
                    Status = taskDto.Status,
                    Priority = taskDto.Priority,
                };

                collection.UpdateTask(updatedTask);
                return NoContent();
            }

            catch (InvalidOperationException ex)
            {
                return NotFound();
            }

            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /// <summary>
        /// Deletes a task.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(Guid id) 
        {
            try
            {
                var existingTask = collection.GetTaskById(id);

                if (existingTask.Status != Status.Completed)
                {
                    return BadRequest("Delete is only allowed for completed tasks.");
                }

                collection.DeleteTask(id);

                return NoContent();

            }

            catch (InvalidOperationException ex)
            {
                return NotFound();
            }

            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
