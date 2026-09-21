using System.ComponentModel.DataAnnotations.Schema;
using team_management_system.DAL.Entities;

namespace team_management_system.DTO
{
    public class TaskCommentDTO
    {
        public int Id { get; set; }

        public int TaskId { get; set; } // কোন টাস্কের কমেন্ট

        public int? ParentId { get; set; } // null হলে মূল কমেন্ট, আইডি থাকলে রিপ্লাই

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // EF Core Self-Referencing Relationship
        [ForeignKey(nameof(ParentId))]
        public TaskComment? ParentComment { get; set; }

        public ICollection<TaskComment> Replies { get; set; } = new List<TaskComment>();
    }

    public class CreateCommentDto
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; } // মূল কমেন্ট হলে null, রিপ্লাই হলে যার রিপ্লাই তার id
        public string Comment { get; set; }
    }

    public class TaskCommentResponseDto
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public int UserId { get; set; }
        public long? ParentId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // এর ভেতরে থাকা সব Sub-replies এর লিস্ট
        public List<TaskCommentResponseDto> Replies { get; set; } = new List<TaskCommentResponseDto>();
    }
}
