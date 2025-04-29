using Microsoft.AspNetCore.Mvc;
using XPEdu.Client.Api.Infra.Repositories;
using XPEdu.Client.Api.Models.DTOs;

namespace XPEdu.Client.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> Get()
        {
            var result = await _clientRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewClientDTO newClientDTO)
        {
            var result = await _clientRepository.AddAsync(newClientDTO);

            if (result == 0)
            {
                return BadRequest("Error creating client");
            }

            return CreatedAtAction(nameof(Post), new { id = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromBody] NewClientDTO newClientDTO)
        {
            await _clientRepository.UpdateAsync(id, newClientDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            await _clientRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Route("total")]
        public async Task<ActionResult<long>> GetTotal()
        {
            var result = await _clientRepository.GetTotalRecords();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetById(long id)
        {
            var result = await _clientRepository.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("filter-by-name/{name}")]
        public async Task<ActionResult<ClientDTO>> GetByName(string name)
        {
            var result = await _clientRepository.GetByNameFilter(name);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
