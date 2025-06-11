using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientTaskTracker.Core.Interfaces;
using PatientTaskTracker.Core.Models;
using PatientTaskTracker.API.DTOs;
using PatientTaskTracker.Core.Managers;

namespace PatientTaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly TaskManagerAsync _taskItemManager;

        public TaskItemController(TaskManagerAsync taskItemManager)
        {
            _taskItemManager = taskItemManager;
        }
        // Define your action methods here, e.g., GetAllTaskItems, GetTaskItemById, CreateTaskItem, etc.
        // Example:
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAllTaskItems()
        // {
        //     var taskItems = await _taskItemManager.GetAllTaskItemsAsync();
        //     return Ok(taskItems);
        //  

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAllTaskItems()
        {
            try
            {
                var taskItems = await _taskItemManager.GetAllTasksAsync();
                if (taskItems == null || !taskItems.Any())
                {
                    return NotFound("No task items found.");
                }

                var taskItemDtos = taskItems.Select(t => new TaskItemDto(
                    t.TaskId,
                    t.PatientId,
                    t.Description,
                    t.DueDate,
                    t.IsCompleted
                ));
                return Ok(taskItemDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving task items: " + ex.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<TaskItemDto>> GetTaskItemById(int id)
        {
            try
            {
                var taskItem = await _taskItemManager.GetTaskByIdAsync(id);
                if (taskItem == null)
                {
                    return NotFound($"Task item with ID {id} not found.");
                }

                var taskItemDto = new TaskItemDto(
                    taskItem.TaskId,
                    taskItem.PatientId,
                    taskItem.Description,
                    taskItem.DueDate,
                    taskItem.IsCompleted
                );
                return Ok(taskItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the task item: " + ex.Message);
            }
        }

        

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTaskItem([FromBody] CreateTaskItemDto createTaskItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _taskItemManager.AddTaskAsync(createTaskItemDto.PatientId, createTaskItemDto.Description,
                    createTaskItemDto.DueDate);

                return Ok(new { message = "TaskItem created successfully" });

            }

            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the task item: " + ex.Message + ex.InnerException?.Message);
            }


        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateTaskItem(int id, [FromBody] UpdateTaskItemDto updateTaskItemDto)
        {
            try
            {




                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }

                var success = await _taskItemManager.UpdateTaskAsync(id, updateTaskItemDto.PatientId,
                    updateTaskItemDto.Description, updateTaskItemDto.DueDate);

                if (!success)
                {
                    return NotFound($"Task item with ID {id} not found or could not be updated.");
                }

                return NoContent();

            }

            catch (Exception ex)

            {
                return StatusCode(500, "An error occurred while updating the task item: " + ex.Message + ex.InnerException?.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaskItem(int id)
        {
            try
            {
                var success = await _taskItemManager.RemoveTaskAsync(id);

                if (!success)
                {
                    return NotFound($"Task item with ID {id} not found or could not be deleted.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the task item: " + ex.Message);
            }
        }
    }
}
