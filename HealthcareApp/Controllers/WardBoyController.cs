using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardBoyController : ControllerBase
    {
        private readonly IServiceRepository<WardBoy, int> wbRepo;

        public WardBoyController(IServiceRepository<WardBoy, int> repo)
        {
            wbRepo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
                response = wbRepo.GetRecords();
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
                ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
                response = wbRepo.GetRecord(id);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(WardBoy wb)
        {
            try
            {
                ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
                response = wbRepo.CreateRecord(wb);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, WardBoy wb)
        {

            try
            {
                ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
                response = wbRepo.UpdateRecord(id, wb);
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
                ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
                response = wbRepo.DeleteRecord(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Occurred {ex.Message}");
            }
        }
    }
}
