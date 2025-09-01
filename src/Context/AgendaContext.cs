using System.Collections.Generic;
using AgendaContatosMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatosMVC.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        public DbSet<Contato> Contatos { get; set; }
    }
}
