using BancoBari.Subscriber_Domain.Dto.PublisherMensagem;
using BancoBari.Subscriber_Domain.Entities;
using BancoBari.Subscriber_Domain.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using repository = BancoBari.Subscriber_Repository.Context;

namespace BancoBari.Subscriber_Repository.Repository.Queued
{
    public class QueuedRepository : IQueuedRepository
    {
        private readonly repository.Context _db;
        public QueuedRepository(repository.Context db)
        {
            _db = db;
        }
        public async Task<bool> Inserir(QueuedObject request)
        {
            if (request != null && Selecionar(request.MensagemId).Result == null)
            {
                _db.Queued.Add(request);
                await _db.SaveChangesAsync();
                return true;
            }
            UpdateMensagemIntegrada(request.MensagemId);
            return false;
        }
        private async void UpdateMensagemIntegrada(Guid mensagemId)
        {
            var mensagem = await _db.Mensagem.FirstOrDefaultAsync(x => x.Id == mensagemId);
            if(mensagem != null)
                mensagem.Integrado = true;

            _db.SaveChanges();
        }
        public async Task<QueuedObject> Selecionar(Guid id)
        {
           var response = await _db.Queued.FirstOrDefaultAsync(x => x.MensagemId == id);
            return response;
        }

        public async Task<List<PublisherMensagemResponseDto>> SelecionarQuantidadeMensagens()
        {
            var query = await(from s in _db.Sistema
                              join q in _db.Queued on s.Id equals q.SistemaId 
                              group s by new { s.Id, s.Nome } into sq
                              select new PublisherMensagemResponseDto
                              {
                                  PublisherDescricao = sq.Key.Nome,
                                  PublisherId = sq.Key.Id,
                                  QntMensagens = sq.Count(),
                                  QtdProcessadas = (from x in _db.Mensagem
                                                    where x.Integrado == true && x.SistemaId == sq.Key.Id
                                                    select x).Count()
                              }).ToListAsync();
            return query;
        }
    }
}
