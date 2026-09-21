using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.DAL.Repositories
{
    public class TaskCommentRepository : Repository<TaskComment>, ITaskCommentRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<TaskComment> _dbSet;
        public TaskCommentRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TaskComment>();
        }
        public async Task<List<TaskCommentResponseDto>> GetTaskCommentsTreeAsync(long taskId)
        {
            var allComments = await _dbContext.TaskComments.Where(c => c.TaskId == taskId).OrderBy(c => c.CreatedAt).ToListAsync();

            var commentDist = allComments.ToDictionary(c => c.Id, c => new TaskCommentResponseDto
            {
                Id = c.Id,
                TaskId = c.TaskId,
                ParentId = c.ParentId,
                UserId = c.UserId,
                Comment = c.Comment,
                CreatedAt = (DateTime)c.CreatedAt,
                Replies = new List<TaskCommentResponseDto>()
            });
            
            var rootComments = new List<TaskCommentResponseDto>();
            foreach (var comment in allComments)
            {
                var dto = commentDist[comment.Id];
                // যদি এটি কোনো কমেন্টের Reply হয়, তবে মূল Parent-এর Replies তালিকায় যোগ হবে
                if (comment.ParentId.HasValue && commentDist.TryGetValue(comment.ParentId.Value, out var parentDto))
                {
                    parentDto.Replies.Add(dto);
                }
                else
                {
                    // যদি ParentId null হয়, তবে এটি Root/Main Comment
                    rootComments.Add(dto);
                }
            }
            return rootComments;
        }
    }
}
