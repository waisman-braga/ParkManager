using System.ComponentModel.DataAnnotations;

namespace ParkManager.API.DTOs
{
    public class MensalistaDto
    {
        public Guid Id { get; set; }

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

        public ClienteDto? Cliente { get; set; }
        public VeiculoDto? Veiculo { get; set; }
    }

    public class MensalistaCreateDto
    {
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
    }

    public class MensalistaUpdateDto
    {
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
    }
}
