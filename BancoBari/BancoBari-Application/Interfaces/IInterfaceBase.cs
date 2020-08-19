using BancoBari_Application.Result;
using System;
using System.Threading.Tasks;

namespace BancoBari_Application.Interfaces
{
    public interface IInterfaceBase<TResponse, TRequest> 
        where TResponse: TResult
        where TRequest: class
    {
        Task<TResult> Selecionar(Guid id);
        Task<TResult> SelecionarTodosNaoIntegrados();
        Task<TResult> Inserir(TRequest request);
        Task<TResult> Atualizar(TRequest request);
        Task<TResult> Excluir(Guid id);
    }
}
