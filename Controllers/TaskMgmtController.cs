using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_management_system.BAL.Interfaces;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskMgmtController : ControllerBase
    {
        private readonly ITaskMgmtService _taskMgmtService;
        private readonly APIResponse _apiResponse;
        public TaskMgmtController(ITaskMgmtService taskMgmtService)
        {
            _taskMgmtService = taskMgmtService;
            _apiResponse = new();
        }


        [HttpPost]
        [Route("create-task")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateTask(TaskMgmtDTO dto)
        {
            try
            {
                var taskId = await _taskMgmtService.CreateTaskAsync(dto);
                if (taskId > 0)
                {
                    _apiResponse.Message.Add("Task created successfully");
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    return _apiResponse;
                }
                else
                {
                    _apiResponse.Message.Add("Failed to create task");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
            }
            catch (Exception ex)
            {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _apiResponse.Message.Add(ex.Message);
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("Get-task-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllTaskList()
        {
            try 
            {
                var taskList = await _taskMgmtService.GetAllTaskAsync();
                if (taskList == null)
                {
                    _apiResponse.Message.Add("No Data Found");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Data = taskList;
                _apiResponse.Message.Add("Task List fetched successfully");
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return _apiResponse;
            } 
            catch (Exception ex)
            {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _apiResponse.Message.Add(ex.Message);
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
        }
    }
}
