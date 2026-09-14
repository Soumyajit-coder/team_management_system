using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class TaskMgmtService : ITaskMgmtService
    {
        private ITaskMgmtRepository _taskMgmtRepository;
        private IMapper _mapper;

        public TaskMgmtService(ITaskMgmtRepository taskMgmtRepository, IMapper mapper)
        {
            _taskMgmtRepository = taskMgmtRepository;
            _mapper = mapper;
        }

        public async Task<long> CreateTaskAsync(DTO.TaskMgmtDTO dto)
        {
            var taskEntity = _mapper.Map<TaskMgmt>(dto);
            var createdTask = await _taskMgmtRepository.CreateAsync(taskEntity);
            return createdTask.Id;
        }
        public async Task<List<TaskMgmtDetailsDTO>> GetAllTaskAsync()
        {
            var taskList = await _taskMgmtRepository.GetTaskListAsync();
            return taskList;
        }
    }
}
