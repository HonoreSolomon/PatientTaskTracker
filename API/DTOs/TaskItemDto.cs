using System.ComponentModel.DataAnnotations;

namespace PatientTaskTracker.API.DTOs
{
    public record TaskItemDto(
        int TaskId,
        string Title,
        string Description,
        DateTime DueDate,
        bool IsCompleted
    );

    public record CreateTaskItemDto(
        [Required] [StringLength(100)] string Title,
        [StringLength(500)] string? Description,
        [Required] DateTime Duedate
    );

    public record UpdateTaskItemDto(
        [Required] [StringLength(100)] string Title,
        [StringLength(500)] string? Description,
        [Required] DateTime Duedate,
        [Required] bool IsCompleted
    );

}
