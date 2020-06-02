using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Master.Services.Dtos;
using Todo.Master.Services.Interfaces;

namespace Todo.Master.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TodoController(ITaskService service)
        {
            _taskService = service;
        }

        [HttpGet("getAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var allData = await _taskService.GetAllTasks();
            return Ok(allData);
        }

        [HttpGet("getTaskById/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            return Ok(task);
        }

        [HttpPost("addNewTask")]
        public async Task<IActionResult> AddNewTask([FromBody] TaskCreateViewModel taskModel)
        {
            var newTask = await _taskService.CreateTask(taskModel);
            return CreatedAtAction(nameof(GetTaskById), routeValues: new { id = newTask.Id }, newTask);
        }

        [HttpPut("updateTask/{id}")]
        public async Task<IActionResult> UpdateTask(int id,[FromBody] TaskUpdateViewModel taskModel)
        {
            var result = await _taskService.UpdateTaskState(id,taskModel);
            return Ok(result);
        }

        [HttpDelete("deleteTask/{id}")]
        
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskById(id);
            return Ok(result);
        }
    }

}
