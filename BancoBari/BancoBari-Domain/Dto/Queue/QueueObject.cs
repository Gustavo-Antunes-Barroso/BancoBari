using System;

namespace BancoBari_Domain.Dto.Queue
{
    public class QueueObject
    {
        public QueueObject()
        {
            TransactionId = Guid.NewGuid();
            Data = DateTime.Now;
        }
        public Guid SistemaId { get; set; }
        public Guid MensagemId { get; set; }
        public string MensagemDescricao { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime Data { get; set; }
    }
}
