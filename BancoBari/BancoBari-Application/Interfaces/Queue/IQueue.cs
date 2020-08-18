using BancoBari_Domain.Dto.Queue;

namespace BancoBari_Application.Interfaces.Queue
{
    public interface IQueue
    {
        QueueObject MontarObjeto();
        void Enfileirar();
    }
}
