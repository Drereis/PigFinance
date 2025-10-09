using System.Collections.Generic;
using System.Reflection.Emit;

namespace PigFinance.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Poupanca> Poupanca { get; set; }
        public DbSet<Transaction> Transacao { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(usuario => Usuario.Id);

            modelBuilder.Entity<Transaction>()
                .HasKey(transaction => transaction.Id);

            modelBuilder.Entity<Poupanca>()
                .HasKey(poupanca => poupanca.Id);

            modelBuilder.Entity<Transaction>()
               .HasOne(transaction => transaction.Usuario)
               .WithMany(Usuario => Usuario.Transaction)
               .HasForeignKey(p => p.UsuarioId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
