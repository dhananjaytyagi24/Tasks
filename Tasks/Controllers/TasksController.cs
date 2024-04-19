using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tasks.Models;
using Tasks.Repository;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("api/v1/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly TaskDataStore _taskDataStore;
        private readonly ILogger<TasksController> _logger;
        private readonly ITaskRepository _taskRepository;

        public TasksController(TaskDataStore taskDataStore, ILogger<TasksController> logger, ITaskRepository taskRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskDataStore = taskDataStore ?? throw new ArgumentNullException(nameof(taskDataStore));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskDto>> GetTasks()
        {
            var tasks = _taskRepository.GetTasks();
            if (tasks == null)
            {
                throw new Exception("Failed to get all Tasks");
            }
            return Ok(tasks);
        }

        [HttpGet("{taskId}", Name = "GetTask")]
        public ActionResult<TaskDto> GetTask([FromRoute] Guid taskId)
        {
            if (taskId == Guid.Empty)
            {
                throw new ArgumentNullException($"TaskId was either null or empty: {taskId}");
            }
            var task = _taskRepository.GetTask(taskId.ToString());

            if (task == null)
            {
                _logger.LogInformation($"Task with Id: {taskId} could not be found");
                return NotFound($"Task with Id: {taskId} could not be found");
            }

            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask(CreateTaskDto createTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid create task request");
            }

            var newTask = _taskRepository.CreateTask(createTaskDto);

            return CreatedAtRoute("GetTask", new { taskId = newTask.Id }, newTask);

        }

        [HttpPut("{taskId}")]
        public IActionResult UpdateTask([FromRoute] Guid taskId, UpdateTaskDto updateTaskDto)
        {
            var task = _taskDataStore.Tasks.Where(x => x.Id == taskId).SingleOrDefault();
            if (task == null)
            {
                return NotFound($"Task with Id: {taskId} could not be found");
            }

            task.Description = updateTaskDto.Description;
            task.Importance = updateTaskDto.Importance;
            task.DaysTaken = updateTaskDto.DaysTaken;
            task.IsCompleted = updateTaskDto.IsCompleted;

            return NoContent();
        }

        //[HttpPatch("{taskId}")]
        //public IActionResult PatchTask([FromRoute] Guid taskId, JsonPatchDocument<UpdateTaskDto> jsonPatchDocument)
        //{
        //    var task = _taskDataStore.Tasks.Where(x => x.Id == taskId).SingleOrDefault();
        //    if (task == null)
        //    {
        //        return NotFound($"Task with Id: {taskId} could not be found");
        //    }

        //    UpdateTaskDto updateTaskDto = new UpdateTaskDto()
        //    {
        //        Description = task.Description,
        //        Importance = task.Importance,
        //        DaysTaken = task.DaysTaken,
        //        IsCompleted = task.IsCompleted
        //    };

        //    // Sending ModelState as a parameter to check for any errors after applying the json patch doc 
        //    jsonPatchDocument.ApplyTo(updateTaskDto, ModelState);
        //    task.Description = updateTaskDto.Description;
        //    task.Importance = updateTaskDto.Importance;
        //    task.DaysTaken = updateTaskDto.DaysTaken;
        //    task.IsCompleted = updateTaskDto.IsCompleted;

        //    return NoContent();
        //}

        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask([FromRoute] Guid taskId)
        {
            var task = _taskDataStore.Tasks.Where(x => x.Id == taskId).SingleOrDefault();

            if (task == null)
            {
                return NotFound($"Task with Id: {taskId} could not be found");
            }

            _taskDataStore.Tasks.Remove(task);

            return NoContent();
        }
    }
}
