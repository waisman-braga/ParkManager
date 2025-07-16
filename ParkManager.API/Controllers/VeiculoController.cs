using Microsoft.AspNetCore.Mvc;
using ParkManager.Domain;
using ParkManager.Domain.Interfaces;

namespace ParkManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculo _veiculoRepository;

        public VeiculoController(IVeiculo veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var veiculos = await _veiculoRepository.ObterTodosAsync();
            return Ok(veiculos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(Guid id)
        {
            var veiculo = await _veiculoRepository.ObterPorIdAsync(id);
            if (veiculo == null)
                return NotFound();
            return Ok(veiculo);
        }

        [HttpGet("placa/{placa}")]
        public async Task<IActionResult> GetPorPlaca(string placa)
        {
            var veiculo = await _veiculoRepository.ObterPorPlacaAsync(placa);
            if (veiculo == null)
                return NotFound();
            return Ok(veiculo);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetPorCliente(Guid clienteId)
        {
            var veiculos = await _veiculoRepository.ObterPorClienteAsync(clienteId);
            return Ok(veiculos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Veiculo veiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar se a placa já existe
            if (await _veiculoRepository.PlacaExisteAsync(veiculo.Placa))
                return BadRequest("Placa já existe no sistema");

            var veiculoAdicionado = await _veiculoRepository.AdicionarAsync(veiculo);
            return CreatedAtAction(nameof(GetPorId), new { id = veiculoAdicionado.Id }, veiculoAdicionado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Veiculo veiculo)
        {
            if (id != veiculo.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar se a placa já existe para outro veículo
            if (await _veiculoRepository.PlacaExisteAsync(veiculo.Placa, veiculo.Id))
                return BadRequest("Placa já existe no sistema");

            var veiculoAtualizado = await _veiculoRepository.AtualizarAsync(veiculo);
            return Ok(veiculoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sucesso = await _veiculoRepository.DeletarAsync(id);
            if (!sucesso)
                return NotFound();
            return NoContent();
        }
    }
}