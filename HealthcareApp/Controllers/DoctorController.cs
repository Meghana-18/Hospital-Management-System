using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IServiceRepository<Doctor, int> docRepo;

        public DoctorController(IServiceRepository<Doctor, int> repo)
        {
            docRepo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
                response = docRepo.GetRecords();
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
                ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
                response = docRepo.GetRecord(id);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(Doctor doc)
        {
            try
            {
                ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
                response = docRepo.CreateRecord(doc);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Doctor doc)
        {

            try
            {
                ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
                response = docRepo.UpdateRecord(id, doc);
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
                ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
                response = docRepo.DeleteRecord(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Occurred {ex.Message}");
            }
        }


        
    }
}
