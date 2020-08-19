using BancoBari.Subscriber_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BancoBari.Subscriber_Repository.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<QueuedObject> Queued { get; set; }
        public DbSet<Sistema> Sistema { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }
        
    }
}
