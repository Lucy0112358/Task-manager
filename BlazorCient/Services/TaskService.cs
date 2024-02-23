using DataModels;
using System.Net.Http.Json;

namespace BlazorCient.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;
        public TaskService(HttpClient httpClient)
        {

            this._httpClient = httpClient;

        }

        public async Task<IEnumerable<UserTask>> getAllTasks()
        {
           return await _httpClient.GetFromJsonAsync<UserTask[]>("http://localhost:5030/api/Task/getAllTasks/");
        }

        public async Task deleteTask(int id)
        {
            await _httpClient.DeleteAsync($"http://localhost:5030/api/Task/deleteTask{id}");
        }

        public async Task updateTask(int id, UserTask updateTaskDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5030/api/Task/UpdateTask/{id}", updateTaskDto);

            if (response.IsSuccessStatusCode)
            {
              await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error updating task: {errorMessage}");
            }
        }

        public async Task PostTask(UserTask userTask)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5030/api/Task/PostTask", userTask);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error posting task: {errorMessage}");
            }
        }

    }
}
