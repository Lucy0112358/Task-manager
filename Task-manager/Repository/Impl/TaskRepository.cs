using System.Data.SqlClient;
using System.Data;
using Task_Management.Models;
using Task_Management.Repository.Interfaces;
using Task_Management.Entities;

namespace Task_Management.Repository.Impl
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IConfiguration _configuration;

        private string _connectionString = string.Empty;
        public TaskRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<RequestDto> GetAllTasks()
        {
            List<RequestDto> tasks = new List<RequestDto>();
            string query = "select t.title as name, t.description, t.id, t.status_id, s.title as statusId from Tasks t join status s on t.status_id = s.id ";
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(_connectionString);
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {                        
                        while (reader.Read())
                        {
                            var task = new RequestDto();
                            task.title = reader.GetString(reader.GetOrdinal("name"));
                            task.description = reader.GetString(reader.GetOrdinal("description"));
                            task.status_id = reader.GetInt32(reader.GetOrdinal("status_id"));
                            task.status = reader.GetString(reader.GetOrdinal("statusId"));
                            task.id = reader.GetInt32(reader.GetOrdinal("id"));
                            tasks.Add(task);
                        } 
                        myCon.Close();
                    }
                }
            }
            return tasks;
        }

        public async Task<int> CreateTask(UserTask obj)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Tasks VALUES (@Title, @Description, @StatusId); SELECT SCOPE_IDENTITY();", con))
                    {
                        cmd.Parameters.AddWithValue("@Title", obj.title);
                        cmd.Parameters.AddWithValue("@Description", obj.description);
                        cmd.Parameters.AddWithValue("@StatusId", obj.status_id);

                        object result = await cmd.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int newTaskId))
                        {
                            return newTaskId;
                        }
                    }
                }

                return default;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UpdateTaskDto> UpdateTask(UserTask obj, int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Tasks SET title=@Title, description=@Description, status_id=@Status_id WHERE id=@Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Title", obj.title);
                        cmd.Parameters.AddWithValue("@Description", obj.description);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Status_id", obj.status_id);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    return new UpdateTaskDto
                    {
                        title = obj.title,
                        description = obj.description,
                        status_id = obj.status_id
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteTask(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("DELETE FROM Tasks WHERE id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
