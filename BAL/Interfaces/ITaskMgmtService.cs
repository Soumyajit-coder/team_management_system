using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface ITaskMgmtService
    {
        Task<long> CreateTaskAsync(TaskMgmtDTO dto);
        Task<List<TaskMgmtDetailsDTO>> GetAllTaskAsync();
    }
}
