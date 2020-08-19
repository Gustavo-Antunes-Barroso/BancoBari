using AutoMapper;
using BancoBari_Application.Interfaces.Mensagem;
using BancoBari_Application.Result;
using BancoBari_Domain.Dto.Mensagem;
using BancoBari_Domain.RepositoryInterfaces.Mensagem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using entity = BancoBari_Domain.Entities;

namespace BancoBari_Application.Implementation.Mensagem
{
    public class MensagensAppServices : IMensagensAppServices
    {
        private readonly IMensagensRepository _mensagensRepository;
        private readonly IMapper _mapper;
        public MensagensAppServices(IMensagensRepository mensagensRepository, IMapper mapper)
        {
            _mensagensRepository = mensagensRepository;
            _mapper = mapper;
        }
        public async Task<TResult> Atualizar(MensagemDto request)
        {
            var response = new TResult();
            response.Object = request;

            var obj = _mapper.Map<entity.Mensagem>(request);

            if (obj.IsValid())
                response.Success = await _mensagensRepository.Atualizar(obj);

            if (!response.Success)
                response.Errors.Add("Mensagem não encontrada!");

            return response;
        }

        public async Task<TResult> Excluir(Guid id)
        {
            var response = new TResult();
            response.Success = await _mensagensRepository.Excluir(id);

            if (!response.Success)
                response.Errors.Add("Mensagem não encontrada!");

            return response;
        }

        public async Task<TResult> Inserir(MensagemDto request)
        {
            var response = new TResult();
            response.Object = request;

            var obj = _mapper.Map<entity.Mensagem>(request);

            if (obj.IsValid())
                response.Success = await _mensagensRepository.Inserir(obj);

            if (!response.Success)
                response.Errors.Add("Não foi possível adicionar a mensagem!");

            return response;
        }

        public async Task<TResult> Selecionar(Guid id)
        {
            var response = new TResult();
            var obj = await _mensagensRepository.Selecionar(id);

            if(obj == null)
                response.Errors.Add("Mensagem não encontrada!");
            else
            {
                response.Object = _mapper.Map<MensagemDto>(obj);
                response.Success = true;
            }

            return response;
        }

        public async Task<TResult> SelecionarTodosNaoIntegrados()
        {
            var response = new TResult();
            var obj = await _mensagensRepository.SelecionarTodosNaoIntegrados();

            if (obj.Count <= 0)
            {
                InserirMensagemParaIntegracaoContinua();
                response.Errors.Add("Nenhuma mensagem encontrada");
            }
            else
            {
                response.Object = _mapper.Map<List<MensagemDto>>(obj);
                response.Success = true;
            }
            return response;
        }
        public void InserirMensagemParaIntegracaoContinua()
        {
            _mensagensRepository.InserirMensagemParaIntegracaoContinua();
        }
    }
}
