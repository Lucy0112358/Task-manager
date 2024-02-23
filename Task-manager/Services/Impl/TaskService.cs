using Task_Management.Entities;
using Task_Management.Models;
using Task_Management.Repository.Interfaces;
using Task_Management.Services.Interfaces;

namespace Task_Management.Services.Impl
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repo) {
            _repository = repo;
        }

        public List<RequestDto> GetAllTasks()
        {
            return _repository.GetAllTasks();
        }

        public Task<int> CreateTask(UserTask request)
        {
            return  _repository.CreateTask(request);
        }

        public Task<UpdateTaskDto> UpdateTask(UserTask request, int id)
        {
            return _repository.UpdateTask(request, id);
        }
        public async Task DeleteTask(int id)
        {
            await _repository.DeleteTask(id);
        }
    }
}
