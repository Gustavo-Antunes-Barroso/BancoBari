using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BancoBari.Subscriber_Domain.Entities
{
    [Table("Sistema")]
    public class Sistema
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
