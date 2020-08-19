using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BancoBari.Subscriber_Domain.Entities
{
    [Table("Mensagem")]
    public class Mensagem
    {
        [Key]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Guid SistemaId { get; set; }
        public bool Integrado { get; set; }
    }
}
