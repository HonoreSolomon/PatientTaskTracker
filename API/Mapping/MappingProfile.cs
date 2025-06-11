using AutoMapper;
using PatientTaskTracker.API.DTOs;
using PatientTaskTracker.Core.Models;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.Tasks, opt => 
                    opt.MapFrom(src => src.Tasks));

            CreateMap<CreatePatientDto, Patient>();
            CreateMap<UpdatePatientDto, Patient>();
            CreateMap<TaskItem, TaskItemDto>();
            CreateMap<CreateTaskItemDto, TaskItem>();
            CreateMap<UpdateTaskItemDto, TaskItem>();
        }
    }
}
