using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoBari_Domain.Entities
{
    [Table("Mensagem")]
    public class Mensagem
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }

        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(Descricao) || Id == Guid.Empty ? false : true;
        }
    }
}
