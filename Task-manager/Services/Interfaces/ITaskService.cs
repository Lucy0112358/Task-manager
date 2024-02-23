using Task_Management.Entities;
using Task_Management.Models;

namespace Task_Management.Services.Interfaces
{
    public interface ITaskService
    {
        public List<RequestDto> GetAllTasks();
        public Task<int> CreateTask(UserTask request);  
        public Task<UpdateTaskDto> UpdateTask(UserTask request, int id);
        Task DeleteTask(int id);
    }
}
