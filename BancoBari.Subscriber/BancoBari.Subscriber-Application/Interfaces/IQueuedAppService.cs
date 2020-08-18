using BancoBari.Subscriber_Domain.Entities;

namespace BancoBari.Subscriber_Application.Interfaces
{
    public interface IQueuedAppService
    {
        void BuscarNaFila();
    }
}
