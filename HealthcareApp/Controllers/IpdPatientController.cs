using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpdPatientController : ControllerBase
    {
        private readonly IServiceRepository<IpdPatient, int> ipdRepo;

        public IpdPatientController(IServiceRepository<IpdPatient, int> repo)
        {
            ipdRepo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
                response = ipdRepo.GetRecords();
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
                ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
                response = ipdRepo.GetRecord(id);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(IpdPatient ipd)
        {
            try
            {
                ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
                response = ipdRepo.CreateRecord(ipd);
                response = ipdRepo.CreateRecord(ipd);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, IpdPatient ipd)
        {

            try
            {
                ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
                response = ipdRepo.UpdateRecord(id, ipd);
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
                ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
                response = ipdRepo.DeleteRecord(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Occurred {ex.Message}");
            }
        }
        
    }
}
