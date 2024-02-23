using Task_Management.Entities;
using Task_Management.Models;

namespace Task_Management.Repository.Interfaces
{
    public interface ITaskRepository
    {
        public List<RequestDto> GetAllTasks();
        public Task<int> CreateTask(UserTask obj);
        public Task<UpdateTaskDto> UpdateTask(UserTask obj, int id);
        Task DeleteTask(int id);
    }
}
