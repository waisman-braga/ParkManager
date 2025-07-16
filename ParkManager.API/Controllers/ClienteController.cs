using Microsoft.AspNetCore.Mvc;
using ParkManager.Domain;
using ParkManager.Domain.Interfaces;

namespace ParkManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ICliente _clienteRepository;

        public ClienteController(ICliente clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var clientes = await _clienteRepository.ObterTodosAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteAdicionado = await _clienteRepository.AdicionarAsync(cliente);
            return CreatedAtAction(nameof(GetPorId), new { id = clienteAdicionado.Id }, clienteAdicionado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteAtualizado = await _clienteRepository.AtualizarAsync(cliente);
            return Ok(clienteAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sucesso = await _clienteRepository.DeletarAsync(id);
            if (!sucesso)
                return NotFound();
            return NoContent();
        }
    }
}