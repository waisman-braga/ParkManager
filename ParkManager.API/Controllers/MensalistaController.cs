using Microsoft.AspNetCore.Mvc;
using ParkManager.Domain;
using ParkManager.Domain.Interfaces;
using ParkManager.API.Services;

namespace ParkManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MensalistaController : ControllerBase
    {
        private readonly IMensalista _mensalistaRepository;
        private readonly ValidationService _validationService;

        public MensalistaController(IMensalista mensalistaRepository, ValidationService validationService)
        {
            _mensalistaRepository = mensalistaRepository;
            _validationService = validationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var mensalistas = await _mensalistaRepository.ObterTodosAsync();
            return Ok(mensalistas);
        }

        [HttpGet("ativos")]
        public async Task<IActionResult> GetAtivos()
        {
            var mensalistas = await _mensalistaRepository.ObterAtivosAsync();
            return Ok(mensalistas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(Guid id)
        {
            var mensalista = await _mensalistaRepository.ObterPorIdAsync(id);
            if (mensalista == null)
                return NotFound();
            return Ok(mensalista);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetPorCliente(Guid clienteId)
        {
            var mensalista = await _mensalistaRepository.ObterPorClienteAsync(clienteId);
            if (mensalista == null)
                return NotFound();
            return Ok(mensalista);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Mensalista mensalista)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar se o cliente existe
            if (!await _validationService.ClienteExisteAsync(mensalista.ClienteId))
                return BadRequest("Cliente não encontrado.");

            // Validar se o veículo existe
            if (!await _validationService.VeiculoExisteAsync(mensalista.VeiculoId))
                return BadRequest("Veículo não encontrado.");

            // Validar se o veículo pertence ao cliente
            if (!await _validationService.VeiculoPertenceAoClienteAsync(mensalista.VeiculoId, mensalista.ClienteId))
                return BadRequest("Veículo não pertence ao cliente informado.");

            // Validar se o cliente já não é mensalista
            if (await _validationService.ClienteJaEMensalistaAsync(mensalista.ClienteId))
                return BadRequest("Cliente já possui contrato de mensalista ativo.");

            // Validar datas
            if (!_validationService.DataVencimentoValida(mensalista.DataInicio, mensalista.DataVencimento))
                return BadRequest("Data de vencimento deve ser posterior à data de início.");

            var mensalistaAdicionado = await _mensalistaRepository.AdicionarAsync(mensalista);
            return CreatedAtAction(nameof(GetPorId), new { id = mensalistaAdicionado.Id }, mensalistaAdicionado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Mensalista mensalista)
        {
            if (id != mensalista.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mensalistaAtualizado = await _mensalistaRepository.AtualizarAsync(mensalista);
            return Ok(mensalistaAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sucesso = await _mensalistaRepository.DeletarAsync(id);
            if (!sucesso)
                return NotFound();
            return NoContent();
        }
    }
}