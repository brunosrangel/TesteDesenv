namespace TesteImposto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NotaFiscal")]
    public partial class NotaFiscal
    {
        public int Id { get; set; }

        public int? NumeroNotaFiscal { get; set; }

        public int? Serie { get; set; }

        [StringLength(50)]
        public string NomeCliente { get; set; }

        [StringLength(50)]
        public string EstadoDestino { get; set; }

        [StringLength(50)]
        public string EstadoOrigem { get; set; }
    }
}
