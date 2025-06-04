using System.ComponentModel.DataAnnotations;
namespace PatientTaskTracker.API.DTOs
{
        public record PatientDto(
        
                int PatientId,
                string FirstName,
                string LastName, 
                List<TaskItemDto> Tasks
        );

        public record  CreatePatientDto(
            [Required][StringLength(50)] string FirstName,
            [Required][StringLength(50)] string LastName
        );


        public record UpdatePatientDto(
            [Required][StringLength(50)] string FirstName,
            [Required][StringLength(50)] string LastName
        );
}
