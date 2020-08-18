using BancoBari.Subscriber_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BancoBari.Subscriber_Crosscutting.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<QueuedObject> Queued { get; set; }
    }
}
