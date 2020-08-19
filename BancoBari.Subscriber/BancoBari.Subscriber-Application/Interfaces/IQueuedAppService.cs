using BancoBari.Subscriber_Domain.Dto.PublisherMensagem;
using BancoBari.Subscriber_Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BancoBari.Subscriber_Application.Interfaces
{
    public interface IQueuedAppService
    {
        void BuscarNaFila();
        Task<List<PublisherMensagemResponseDto>>SelecionarQuantidadeMensagens();
    }
}
