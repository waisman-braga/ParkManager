using System;
using System.ComponentModel.DataAnnotations;

namespace ParkManager.Domain
{
    public class Veiculo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Placa é obrigatória")]
        [StringLength(8, ErrorMessage = "Placa deve ter no máximo 8 caracteres")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Modelo é obrigatório")]
        [StringLength(50, ErrorMessage = "Modelo deve ter no máximo 50 caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public Guid ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}