using Tasks.Models;

namespace Tasks.Repository
{
    public interface ITaskRepository
    {
        public IEnumerable<TaskDto> GetTasks();

        public TaskDto GetTask(string taskId);

        public TaskDto CreateTask(CreateTaskDto createTaskDto);
    }
}
