using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Master.Data.Entities;
using Todo.Master.Data.Storage;
using Todo.Master.Services.Dtos;
using Todo.Master.Services.Interfaces;

namespace Todo.Master.Services.Imp
{
    public class TaskService : ITaskService
    {
        private readonly Todo_MasterContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;
        public TaskService(Todo_MasterContext context, IMapper mapper, ILogger<TaskService> logger)
        {
            _dbContext = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TaskListAndDetailViewModel> CreateTask(TaskCreateViewModel task)
        {
            var newTask = _mapper.Map<Data.Entities.Task>(task);
            _dbContext.Tasks.Add(newTask);
            await _dbContext.SaveChangesAsync();

            var taskModel = _mapper.Map<TaskListAndDetailViewModel>(newTask);
            _logger.LogInformation($"TaskService Create Task: id = {taskModel.Id}, title = {taskModel.Title}, state = {taskModel.State} from master");
            return taskModel;
        }

        public async Task<bool> DeleteTaskById(int id)
        {
            var task = await _dbContext.Tasks.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (task == null) return false;


            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"TaskService Delete Task: id = {id} from master");
            return true;
        }

        public async Task<IEnumerable<TaskListAndDetailViewModel>> GetAllTasks()
        {
            var allTasks = await _dbContext.Tasks.Select(t => _mapper.Map<TaskListAndDetailViewModel>(t)).ToListAsync();
            return allTasks;
        }

        public async Task<TaskListAndDetailViewModel> GetTaskById(int id)
        {
            var task = await _dbContext.Tasks.Where(w => w.Id == id)
                .Select(s => _mapper.Map<TaskListAndDetailViewModel>(s)).FirstOrDefaultAsync();
            return task;
        }

        public async Task<bool> UpdateTaskState(int id,TaskUpdateViewModel task)
        {
            var updateTask = await _dbContext.Tasks.FirstOrDefaultAsync(w => w.Id == id);

            if (updateTask == null)
            {
                return false;
            }

            _mapper.Map(task, updateTask);
            _logger.LogInformation($"TaskService Update Task: id = {id}, state = {task.State} from master");
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
