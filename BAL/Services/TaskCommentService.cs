using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _taskCommentRepository;
        private readonly IMapper _mapper;
        public TaskCommentService(ITaskCommentRepository taskCommentRepository, IMapper mapper)
        {
            _taskCommentRepository = taskCommentRepository;
            _mapper = mapper;
        }
        public async Task<long> CreateTaskCommentAsync(CreateCommentDto dto)
        {
            var taskComment = _mapper.Map<TaskComment>(dto);
            await _taskCommentRepository.CreateAsync(taskComment);
            return taskComment.Id;
        }
        public async Task<List<TaskCommentResponseDto>> GetTaskTreeCommentsAsync(long taskId)
        {
            var comments = await _taskCommentRepository.GetTaskCommentsTreeAsync(taskId);
            var commentDtos = _mapper.Map<List<TaskCommentResponseDto>>(comments);
            // Build the tree structure
            var commentDict = commentDtos.ToDictionary(c => c.Id);
            foreach (var comment in commentDtos)
            {
                if (comment.ParentId.HasValue && commentDict.ContainsKey(comment.ParentId.Value))
                {
                    commentDict[comment.ParentId.Value].Replies.Add(comment);
                }
            }
            // Return only the root comments (those without a parent)
            return commentDtos.Where(c => !c.ParentId.HasValue).ToList();
        }
    }
}
