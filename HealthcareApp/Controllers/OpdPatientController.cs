using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpdPatientController : ControllerBase
    {
        private readonly IServiceRepository<OpdPatient, int> opdRepo;

        public OpdPatientController(IServiceRepository<OpdPatient, int> repo)
        {
            opdRepo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
                response = opdRepo.GetRecords();
                return Ok(response.Records);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
                response = opdRepo.GetRecord(id);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(OpdPatient opd)
        {
            try
            {
                ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
                response = opdRepo.CreateRecord(opd);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, OpdPatient opd)
        {
            
            try
            {
                ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
                response = opdRepo.UpdateRecord(id, opd);
                return Ok(response);
    
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Occurred {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
                response = opdRepo.DeleteRecord(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Occurred {ex.Message}");
            }
        }

        
    }
}
