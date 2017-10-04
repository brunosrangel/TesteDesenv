namespace TesteImposto
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class mdl : DbContext
    {
        public mdl()
            : base("name=mdl")
        {
        }

        public virtual DbSet<NotaFiscal> NotaFiscal { get; set; }
        public virtual DbSet<NotaFiscalItem> NotaFiscalItem { get; set; }

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
