using entity = BancoBari_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BancoBari_Repository.Repository.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<entity.Mensagem> Mensagem { get; set; }
    }
}
