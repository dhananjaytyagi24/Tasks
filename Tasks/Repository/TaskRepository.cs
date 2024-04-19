using Tasks.Models;

namespace Tasks.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDataStore _dataStore;

        public TaskRepository(TaskDataStore dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        public IEnumerable<TaskDto> GetTasks()
        {
            return _dataStore.Tasks;
        }

        public TaskDto GetTask(string taskId)
        {
            if (string.IsNullOrWhiteSpace(taskId))
            {
                throw new Exception("Task Id is null or empty");
            }

            return _dataStore?.Tasks?.Where(x => string.Equals(x.Id.ToString(), taskId, StringComparison.OrdinalIgnoreCase))?.SingleOrDefault();
        }

        public TaskDto CreateTask(CreateTaskDto createTaskDto)
        {
            try
            {
                var newTask = new TaskDto();
                newTask.Id = Guid.NewGuid();
                newTask.CreatedAt = DateTime.Now;
                newTask.IsCompleted = false;
                newTask.Description = createTaskDto.Description;
                newTask.Importance = createTaskDto.Importance;
                newTask.DaysTaken = createTaskDto.DaysTaken;

                _dataStore.Tasks.Add(newTask);
                return newTask;
            }
            catch (Exception)
            {
                throw new Exception("Unable to create new Task");
            }
        }
    }
}
