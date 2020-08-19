using entity = BancoBari_Domain.Entities;
using BancoBari_Domain.RepositoryInterfaces.Mensagem;
using db = BancoBari_Repository.Repository.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BancoBari_Repository.Repository.Mensagem
{
    public class MensagensRepository : IMensagensRepository
    {
        private readonly db.Context _db;
        public MensagensRepository(db.Context db)
        {
            _db = db;
        }
        public async Task<bool> Atualizar(entity.Mensagem request)
        {
            var mensagem = await Selecionar(request.Id);
            if (mensagem == null)
                return false;

            mensagem.Descricao = request.Descricao;
            _db.Mensagem.Update(mensagem);
            await _db.SaveChangesAsync();

            return true;
                
        }

        public async Task<bool> Excluir(Guid id)
        {
            var mensagem = await Selecionar(id);
            if (mensagem == null)
                return false;

             _db.Remove(mensagem);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Inserir(entity.Mensagem request)
        {
            var mensagem = await Selecionar(request.Id);

            if (mensagem == null)
            {
                _db.Mensagem.Add(request); 
                await _db.SaveChangesAsync(); 
                return true;
            }
            return false;
        }

        public void InserirMensagemParaIntegracaoContinua()
        {
            _db.Mensagem.Add(new entity.Mensagem
            {
                Descricao = "Hello World",
                Id = Guid.NewGuid(),
                Integrado = false,
                SistemaId = Guid.Parse("07CCD9AB-C9EE-437A-A992-291417F1F23E")
            });
            _db.SaveChanges();
        }

        public async Task<entity.Mensagem> Selecionar(Guid id)
        {
            var response = await _db.Mensagem.FirstOrDefaultAsync(x => x.Id == id);
            return response;
        }

        public async Task<List<entity.Mensagem>> SelecionarTodosNaoIntegrados()
        {
            var response = await _db.Mensagem.Where(x => x.Integrado == false).ToListAsync();
            return response;
        }
    }
}
