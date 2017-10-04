namespace Imposto.Core
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class md : DbContext
    {
        public md()
            : base("name=md")
        {
        }

        public virtual DbSet<NotaFiscal> NotaFiscals { get; set; }
        public virtual DbSet<NotaFiscalItem> NotaFiscalItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotaFiscal>()
                .Property(e => e.NomeCliente)
                .IsUnicode(false);

            modelBuilder.Entity<NotaFiscal>()
                .Property(e => e.EstadoDestino)
                .IsUnicode(false);

            modelBuilder.Entity<NotaFiscal>()
                .Property(e => e.EstadoOrigem)
                .IsUnicode(false);

            modelBuilder.Entity<NotaFiscalItem>()
                .Property(e => e.Cfop)
                .IsUnicode(false);

            modelBuilder.Entity<NotaFiscalItem>()
                .Property(e => e.TipoIcms)
                .IsUnicode(false);

            modelBuilder.Entity<NotaFiscalItem>()
                .Property(e => e.BaseIcms)
                .HasPrecision(18, 5);

            modelBuilder.Entity<NotaFiscalItem>()
                .Property(e => e.AliquotaIcms)
                .HasPrecision(18, 5);

            modelBuilder.Entity<NotaFiscalItem>()
                .Property(e => e.ValorIcms)
                .HasPrecision(18, 5);

            modelBuilder.Entity<NotaFiscalItem>()
                .Property(e => e.NomeProduto)
                .IsUnicode(false);

            modelBuilder.Entity<NotaFiscalItem>()
                .Property(e => e.CodigoProduto)
                .IsUnicode(false);

            modelBuilder.Entity<NotaFiscalItem>()
                .HasOptional(e => e.NotaFiscalItem1)
                .WithRequired(e => e.NotaFiscalItem2);
        }
    }
}
