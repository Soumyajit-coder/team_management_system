using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface ITaskCommentService
    {
        Task<long> CreateTaskCommentAsync(CreateCommentDto dto);
        Task<List<TaskCommentResponseDto>> GetTaskTreeCommentsAsync(long taskId);
    }
}
