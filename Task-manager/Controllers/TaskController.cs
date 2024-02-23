using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Task_Management.Entities;
using Task_Management.Models;
using Task_Management.Services.Interfaces;

namespace Task_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService service)
        {
            _taskService = service;
        }

        [HttpGet]
        [Route("getAllTasks")]
        public async Task<IActionResult> getAllTasks()
        {
   
            List<RequestDto> tasks = _taskService.GetAllTasks();

            return Ok(tasks);
        }

        [Route("PostTask")]
        [HttpPost]
        public async Task<IActionResult> PostTask([FromBody] UserTask request)
        {
            try
            {
                int id = await _taskService.CreateTask(request);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("UpdateTask/{id}")]
        [HttpPut]  
        public async Task<IActionResult> UpdateTask([FromBody] UserTask request, int id)
        {
            try
            {
                var newid = await _taskService.UpdateTask(request, id);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("deleteTask{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteTask(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
