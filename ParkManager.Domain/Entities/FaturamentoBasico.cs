using System;
using System.ComponentModel.DataAnnotations;

namespace ParkManager.Domain
{
    public class FaturamentoBasico
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "Mês/Ano é obrigatório")]
        public int MesAno { get; set; } // Formato YYYYMM (ex: 202501)

        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Data de vencimento é obrigatória")]
        public DateTime DataVencimento { get; set; }

        public DateTime DataGeracao { get; set; } = DateTime.Now;

        public bool Pago { get; set; } = false;

        // Navigation properties
        public virtual Cliente Cliente { get; set; }
    }
}
