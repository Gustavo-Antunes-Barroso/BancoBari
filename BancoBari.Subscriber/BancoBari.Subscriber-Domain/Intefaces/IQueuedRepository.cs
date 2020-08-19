using BancoBari.Subscriber_Domain.Dto.PublisherMensagem;
using BancoBari.Subscriber_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BancoBari.Subscriber_Domain.Intefaces
{
    public interface IQueuedRepository
    {
        Task<bool> Inserir(QueuedObject request);
        Task<QueuedObject> Selecionar(Guid id);
        Task<List<PublisherMensagemResponseDto>> SelecionarQuantidadeMensagens();
    }
}
