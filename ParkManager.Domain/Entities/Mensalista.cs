using System;
using System.ComponentModel.DataAnnotations;

namespace ParkManager.Domain
{
    public class Mensalista
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "Veículo é obrigatório")]
        public Guid VeiculoId { get; set; }

        [Required(ErrorMessage = "Data de início é obrigatória")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Data de vencimento é obrigatória")]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal Valor { get; set; }

        public bool Ativo { get; set; } = true;

        // Navigation properties
        public virtual Cliente Cliente { get; set; }
        public virtual Veiculo Veiculo { get; set; }
    }
}