using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkManager.Domain
{
    public class Cliente
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(20, ErrorMessage = "Telefone deve ter no máximo 20 caracteres")]
        public string Telefone { get; set; }

        public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
    }
}