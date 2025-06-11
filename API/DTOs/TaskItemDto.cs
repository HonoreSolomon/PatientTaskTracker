using System.ComponentModel.DataAnnotations;

namespace PatientTaskTracker.API.DTOs
{
    public record TaskItemDto(
        int TaskId,
        int PatientId,
        string Description,
        DateTime DueDate,
        bool IsCompleted
    );

    public record CreateTaskItemDto(
        [Required] int PatientId,
        [Required][StringLength(500)] string Description,
        [Required] DateTime DueDate
    );

    public record UpdateTaskItemDto(
        [Required] int PatientId,
        [Required][StringLength(500)] string Description,
        [Required] DateTime DueDate,
        [Required] bool IsCompleted
    );

}
