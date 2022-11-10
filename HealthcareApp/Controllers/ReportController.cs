using System;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository<Report, int> ipdRepo;

        public ReportController(IReportRepository<Report, int> repo)
        {
            ipdRepo = repo;
        }

        [HttpGet("OPDBill/{id}")]
        public IActionResult OpdBill(int id)
        {
            try
            {
                ResponseStatus<decimal> response = new ResponseStatus<decimal>();
                response = ipdRepo.OpdBillValue(id);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("IPDBill/{id}")]
        public IActionResult IpdBill(int id)
        {
            try
            {
                ResponseStatus<decimal> response = new ResponseStatus<decimal>();
                response = ipdRepo.IpdBillValue(id);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("DoctorsBySpecialization/{spec}")]
        public IActionResult GetBySpec(string spec)
        {
            try
            {
                ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
                response = ipdRepo.GetBySpecRecords(spec);
                return Ok(response.Records);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("EmployeeDoctors")]
        public IActionResult GetEmpDoc()
        {
            try
            {
                ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
                response = ipdRepo.GetEmpDocRecords();
                return Ok(response.Records);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("VisitingDoctors")]
        public IActionResult GetVisitingDoc()
        {
            try
            {
                ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
                response = ipdRepo.GetVisitingDocRecords();
                return Ok(response.Records);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("PatientsPerDoctor/{id}")]
        public IActionResult GetByDoctor(int id)
        {
            try
            {
                ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
                response = ipdRepo.GetByDoctorRecords(id);
                return Ok(response.Records);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("PatientsPerNurse/{id}")]
        public IActionResult GetByNurse(int id)
        {
            try
            {
                ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
                response = ipdRepo.GetByNurseRecords(id);
                return Ok(response.Records);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("PatientsPerWard/{ward}")]
        public IActionResult GetByWard(string ward)
        {
            try
            {
                ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
                response = ipdRepo.GetByWardRecords(ward);
                return Ok(response.Records);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("TotalCollection")]
        public IActionResult TotalCollection()
        {
            try
            {
                ResponseStatus<decimal> response = new ResponseStatus<decimal>();
                response = ipdRepo.TotalCollectionValue();
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        

        
    }
}

