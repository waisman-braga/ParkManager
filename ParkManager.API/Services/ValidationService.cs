using ParkManager.Domain.Interfaces;

namespace ParkManager.API.Services
{
    public class ValidationService
    {
        private readonly ICliente _clienteRepository;
        private readonly IVeiculo _veiculoRepository;
        private readonly IMensalista _mensalistaRepository;

        public ValidationService(
            ICliente clienteRepository,
            IVeiculo veiculoRepository,
            IMensalista mensalistaRepository)
        {
            _clienteRepository = clienteRepository;
            _veiculoRepository = veiculoRepository;
            _mensalistaRepository = mensalistaRepository;
        }

        public async Task<bool> ClienteExisteAsync(Guid clienteId)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(clienteId);
            return cliente != null;
        }

        public async Task<bool> VeiculoExisteAsync(Guid veiculoId)
        {
            var veiculo = await _veiculoRepository.ObterPorIdAsync(veiculoId);
            return veiculo != null;
        }

        public async Task<bool> VeiculoPertenceAoClienteAsync(Guid veiculoId, Guid clienteId)
        {
            var veiculo = await _veiculoRepository.ObterPorIdAsync(veiculoId);
            return veiculo != null && veiculo.ClienteId == clienteId;
        }

        public async Task<bool> ClienteJaEMensalistaAsync(Guid clienteId)
        {
            var mensalista = await _mensalistaRepository.ObterPorClienteAsync(clienteId);
            return mensalista != null;
        }

        public bool DataVencimentoValida(DateTime dataInicio, DateTime dataVencimento)
        {
            return dataVencimento > dataInicio;
        }
    }
}
