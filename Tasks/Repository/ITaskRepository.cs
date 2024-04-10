using Tasks.Models;

namespace Tasks.Repository
{
    public interface ITaskRepository
    {
        public IEnumerable<TaskDto> GetTasksAsync();

        public TaskDto GetTask(string taskId);
        //public Task CreateTaskAsync();
    }
}
