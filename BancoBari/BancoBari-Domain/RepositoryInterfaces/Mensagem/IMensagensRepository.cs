using entity = BancoBari_Domain.Entities;
namespace BancoBari_Domain.RepositoryInterfaces.Mensagem
{
    public interface IMensagensRepository : IRepositoryBase<entity.Mensagem>
    {
        void InserirMensagemParaIntegracaoContinua();
    }
}
