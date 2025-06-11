using API.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientTaskTracker.Core.Interfaces;
using PatientTaskTracker.Core.Models;
using PatientTaskTracker.API.DTOs;
using PatientTaskTracker.Core.Managers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientManagerAsync _patientManager;
        private readonly TaskManagerAsync _taskItemManager;
        private readonly IMapper _mapper;

        public PatientsController(PatientManagerAsync patientManager, TaskManagerAsync taskManager, IMapper mapper)
        {
            _patientManager = patientManager;
            _taskItemManager = taskManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
        {
            try
            {
                var patients = await _patientManager.GetAllPatientsAsync();
                if (patients == null || !patients.Any())
                {
                    return NotFound("No patients found.");
                }

                var patientDtos = _mapper.Map<IEnumerable<PatientDto>>(patients);


                //patients.Select(p => new PatientDto(
                //    p.PatientId,
                //    p.FirstName,
                //    p.LastName,
                //    new List<TaskItemDto>()
                //    // Assuming TaskItemDto is defined elsewhere and can be mapped from TaskItem
                //));
                return Ok(patientDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving patients: " + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            try
            {
                var patient = await _patientManager.GetPatientByIdAsync(id);
                if (patient == null)
                {
                    return NotFound($"Patient with ID {id} not found.");
                }
                
                var patientDto = _mapper.Map<PatientDto>(patient);
                //    new PatientDto(
                //    patient.PatientId,
                //    patient.FirstName,
                //    patient.LastName,
                //    new List<TaskItemDto>()
                //);
                return Ok(patientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the patient: " + ex.Message);
            }
        }

        [HttpGet("{patientId}/tasks")]

        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasksByPatientId(int patientId)
        {
            try
            {
                var taskItems = await _taskItemManager.GetTasksByPatientIdAsync(patientId);
                if (taskItems == null || !taskItems.Any())
                {
                    return NotFound($"No task items found for patient with ID {patientId}.");
                }

                var taskItemDtos = taskItems.Select(t => new TaskItemDto(
                    t.TaskId,
                    t.PatientId,
                    t.Description,
                    t.DueDate,
                    t.IsCompleted
                ));

                return Ok(taskItemDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving task items: " + ex.Message);
            }
        }


        [HttpPost]

        public async Task<ActionResult<Patient>> CreatePatient([FromBody] CreatePatientDto createPatientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                await _patientManager.AddPatientAsync(createPatientDto.FirstName, createPatientDto.LastName);

                return Ok(new { message = "Patient created successfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the patient: " + ex.Message);
            }
            
           
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDto updatePatientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var success = await _patientManager.EditPatientAsync(id, updatePatientDto.FirstName, updatePatientDto.LastName);

                if (!success)
                {
                    return NotFound($"Patient with ID {id} not found or could not be updated.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the patient: " + ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            try
            {
                var success = await _patientManager.RemovePatientAsync(id);

                if (!success)
                {
                    return NotFound($"Patient with ID {id} not found.");
                }
                
                return NoContent();
            }
            
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the patient: " + ex.Message);
            }
        }
    }
}
