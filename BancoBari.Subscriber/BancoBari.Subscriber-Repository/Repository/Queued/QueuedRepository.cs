using BancoBari.Subscriber_Crosscutting.Context;
using BancoBari.Subscriber_Domain.Entities;
using BancoBari.Subscriber_Domain.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BancoBari.Subscriber_Repository.Repository.Queued
{
    public class QueuedRepository : IQueuedRepository
    {
        private readonly Context _db;
        public QueuedRepository(Context db)
        {
            _db = db;
        }
        public async Task<bool> Inserir(QueuedObject request)
        {
            if (Selecionar(request.MensagemId).Result == null)
            {
                _db.Queued.Add(request);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<QueuedObject> Selecionar(Guid id)
        {
           var response = await _db.Queued.FirstOrDefaultAsync(x => x.MensagemId == id);
            return response;
        }
    }
}
