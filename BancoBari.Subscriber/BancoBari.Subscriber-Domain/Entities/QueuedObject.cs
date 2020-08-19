using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoBari.Subscriber_Domain.Entities
{
    [Table("Queued")]
    public class QueuedObject
    {
        [Key]
        public Guid TransactionId { get; set; }
        public Guid SistemaId { get; set; }
        public Guid MensagemId { get; set; }
        public string MensagemDescricao { get; set; }
        public DateTime Data { get; set; }
    }
}
