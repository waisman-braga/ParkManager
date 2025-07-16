using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParkManager.Domain;

namespace ParkManager.Domain.Interfaces
{
    public interface IFaturamentoBasico
    {
        Task<IEnumerable<FaturamentoBasico>> ObterTodosAsync();
        Task<FaturamentoBasico> ObterPorIdAsync(Guid id);
        Task<IEnumerable<FaturamentoBasico>> ObterPorMesAnoAsync(int mesAno);
        Task<IEnumerable<FaturamentoBasico>> ObterPendentesAsync();
        Task<FaturamentoBasico> AdicionarAsync(FaturamentoBasico faturamento);
        Task<IEnumerable<FaturamentoBasico>> GerarFaturamentoMensalAsync(int mesAno);
        Task<bool> MarcarComoPagoAsync(Guid faturamentoId);
    }
}
