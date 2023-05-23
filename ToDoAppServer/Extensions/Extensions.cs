using ToDoAppServer.Dtos;
using ToDoAppServer.Models;

namespace ToDoAppServer.Extensions
{
    /// <summary>
    /// Provides extension method for converting a <see cref="TaskObj"/> to a <see cref="TaskObjDto"/>.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts a <see cref="TaskObj"/> instance to a <see cref="TaskObjDto"/> instance.
        /// </summary>
        /// <param name="taskObj">The <see cref="TaskObj"/> to convert.</param>
        /// <returns>The converted <see cref="TaskObjDto"/>.</returns>
        public static TaskObjDto AsDto(this TaskObj taskObj)
        {
            return new TaskObjDto
            {
                ID = taskObj.ID,
                Name = taskObj.Name,
                Priority = taskObj.Priority,
                Status = taskObj.Status,
                CreatedDate = taskObj.CreatedDate,
            };
        }
    }
}
