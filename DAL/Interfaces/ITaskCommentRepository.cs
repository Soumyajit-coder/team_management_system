using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;
using team_management_system.DTO;

namespace team_management_system.DAL.Interfaces
{
    public interface ITaskCommentRepository : IRepository<TaskComment>
    {
        Task<List<TaskCommentResponseDto>> GetTaskCommentsTreeAsync(long taskId);
    }
}
