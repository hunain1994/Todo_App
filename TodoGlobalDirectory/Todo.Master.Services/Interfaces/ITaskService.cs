using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Master.Services.Dtos;

namespace Todo.Master.Services.Interfaces
{
    public interface ITaskService
    {
        //Task<bool> SaveChanges();
        Task<IEnumerable<TaskListAndDetailViewModel>> GetAllTasks();
        Task<TaskListAndDetailViewModel> GetTaskById(int id);
        Task<TaskListAndDetailViewModel> CreateTask(TaskCreateViewModel task);
        Task<bool> UpdateTaskState(int id, TaskUpdateViewModel task);
        Task<bool> DeleteTaskById(int id);
    }
}
