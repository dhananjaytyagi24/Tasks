using Tasks.Enums;
using Tasks.Models;

namespace Tasks
{
    public class TaskDataStore
    {
        public List<TaskDto> Tasks { get; set; }

        public TaskDataStore()
        {
            Tasks = new List<TaskDto> {
                new TaskDto()
                {
                    Id = Guid.Parse("2b1df221-6f44-4a40-8ab5-fbcb5fe95e70"),
                    Description = "Task 1 Description",
                    Importance = ImportanceEnum.Medium,
                    DaysTaken = 1,
                    IsCompleted = false
                },
                new TaskDto()
                {
                    Id = Guid.Parse("d536de88-80f9-4483-a167-ea3815750c7b"),
                    Description = "Task 2 Description",
                    Importance = ImportanceEnum.High,
                    DaysTaken = 2,
                    IsCompleted = false
                }
            };
        }
    }
}
