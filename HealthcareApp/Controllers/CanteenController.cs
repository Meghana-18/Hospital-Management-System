using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanteenController : ControllerBase
    {
        private readonly IEntityRepository<Canteen, int> canRepo;

        public CanteenController(IEntityRepository<Canteen, int> repo)
        {
            canRepo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ResponseStatus<Canteen> response = new ResponseStatus<Canteen>();
                response = canRepo.GetRecords();
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
                ResponseStatus<Canteen> response = new ResponseStatus<Canteen>();
                response = canRepo.GetRecord(id);
                return Ok(response.Record);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

    }
}
