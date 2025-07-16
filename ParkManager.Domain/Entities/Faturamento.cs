using System;
using System.ComponentModel.DataAnnotations;

namespace ParkManager.Domain
{
    public class Faturamento
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Mensalista é obrigatório")]
        public Guid MensalistaId { get; set; }

        [Required(ErrorMessage = "Mês/Ano é obrigatório")]
        public int MesAno { get; set; } // Formato YYYYMM (ex: 202501)

        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Data de vencimento é obrigatória")]
        public DateTime DataVencimento { get; set; }

        public DateTime DataGeracao { get; set; } = DateTime.Now;

        public DateTime? DataPagamento { get; set; }

        public bool Pago { get; set; } = false;

        public string? Observacoes { get; set; }

        // Navigation properties
        public virtual Mensalista Mensalista { get; set; }
    }
}
