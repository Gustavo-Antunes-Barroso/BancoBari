using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BancoBari_Domain.RepositoryInterfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> Selecionar(Guid id);
        Task<List<TEntity>> SelecionarTodosNaoIntegrados();
        Task<bool> Inserir(TEntity request);
        Task<bool> Atualizar(TEntity request);
        Task<bool> Excluir(Guid id);
    }
}
