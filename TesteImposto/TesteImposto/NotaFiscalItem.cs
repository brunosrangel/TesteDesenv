namespace TesteImposto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NotaFiscalItem")]
    public partial class NotaFiscalItem
    {
        public int Id { get; set; }

        public int? IdNotaFiscal { get; set; }

        [StringLength(5)]
        public string Cfop { get; set; }

        [StringLength(20)]
        public string TipoIcms { get; set; }

        public decimal? BaseIcms { get; set; }

        public decimal? AliquotaIcms { get; set; }

        public decimal? ValorIcms { get; set; }

        [StringLength(50)]
        public string NomeProduto { get; set; }

        [StringLength(20)]
        public string CodigoProduto { get; set; }

        public virtual NotaFiscalItem NotaFiscalItem1 { get; set; }

        public virtual NotaFiscalItem NotaFiscalItem2 { get; set; }
    }
}
