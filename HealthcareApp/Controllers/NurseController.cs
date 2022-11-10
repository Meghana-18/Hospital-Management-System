using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NurseController : ControllerBase
    {
        private readonly IServiceRepository<Nurse, int> nurseRepo;

        public NurseController(IServiceRepository<Nurse, int> repo)
        {
            nurseRepo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
                response = nurseRepo.GetRecords();
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
                ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
                response = nurseRepo.GetRecord(id);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(Nurse nur)
        {
            try
            {
                ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
                response = nurseRepo.CreateRecord(nur);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Nurse nur)
        {

            try
            {
                ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
                response = nurseRepo.UpdateRecord(id, nur);
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
                ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
                response = nurseRepo.DeleteRecord(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Occurred {ex.Message}");
            }
        }
    }
}
