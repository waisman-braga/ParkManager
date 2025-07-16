using System.ComponentModel.DataAnnotations;

namespace ParkManager.API.DTOs
{
    public class VeiculoDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Placa é obrigatória")]
        [StringLength(8, ErrorMessage = "Placa deve ter no máximo 8 caracteres")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Modelo é obrigatório")]
        [StringLength(50, ErrorMessage = "Modelo deve ter no máximo 50 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public Guid ClienteId { get; set; }

        public ClienteDto? Cliente { get; set; }
    }

    public class VeiculoCreateDto
    {
        [Required(ErrorMessage = "Placa é obrigatória")]
        [StringLength(8, ErrorMessage = "Placa deve ter no máximo 8 caracteres")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Modelo é obrigatório")]
        [StringLength(50, ErrorMessage = "Modelo deve ter no máximo 50 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public Guid ClienteId { get; set; }
    }

    public class VeiculoUpdateDto
    {
        [Required(ErrorMessage = "Placa é obrigatória")]
        [StringLength(8, ErrorMessage = "Placa deve ter no máximo 8 caracteres")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Modelo é obrigatório")]
        [StringLength(50, ErrorMessage = "Modelo deve ter no máximo 50 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public Guid ClienteId { get; set; }
    }
}
