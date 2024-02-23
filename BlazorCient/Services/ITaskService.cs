using DataModels;

namespace BlazorCient.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<UserTask>> getAllTasks();
        Task deleteTask(int id);
        Task updateTask(int id, UserTask updateTaskDto);
        Task PostTask(UserTask userTask);
    }
}
