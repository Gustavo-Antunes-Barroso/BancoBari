using entity = BancoBari_Domain.Entities;
using BancoBari_Domain.RepositoryInterfaces.Mensagem;
using Crosscutting.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BancoBari_Repository.Repository.Mensagem
{
    public class MensagensRepository : IMensagensRepository
    {
        private readonly Context _db;
        public MensagensRepository(Context db)
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

        public async Task<entity.Mensagem> Selecionar(Guid id)
        {
            var response = await _db.Mensagem.FirstOrDefaultAsync(x => x.Id == id);
            return response;
        }

        public async Task<List<entity.Mensagem>> SelecionarTodos()
        {
            return await _db.Mensagem.ToListAsync();
        }
    }
}
