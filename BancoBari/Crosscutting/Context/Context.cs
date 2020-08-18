using BancoBari_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crosscutting.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Mensagem> Mensagem { get; set; }
    }
}
