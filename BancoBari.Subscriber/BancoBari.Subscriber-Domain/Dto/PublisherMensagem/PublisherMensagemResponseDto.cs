using System;

namespace BancoBari.Subscriber_Domain.Dto.PublisherMensagem
{
    public class PublisherMensagemResponseDto
    {
        public Guid PublisherId { get; set; }
        public int QntMensagens { get; set; }
        public string PublisherDescricao { get; set; }
        public int QtdProcessadas { get; set; }
    }
}
