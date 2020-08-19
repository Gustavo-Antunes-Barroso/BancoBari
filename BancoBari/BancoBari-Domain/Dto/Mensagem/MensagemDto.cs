using System;

namespace BancoBari_Domain.Dto.Mensagem
{
    public class MensagemDto
    {
        public Guid Id { get; set; }
        public Guid SistemaId { get; set; }
        public string Descricao { get; set; }
    }
}
