using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        public string GravaDadosNotaFiscal(Imposto.Core.Domain. NotaFiscal nota)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["mdl"].ConnectionString;
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            var cnn = new SqlConnection();
            cnn.ConnectionString = connectionString;
            var cmd = new SqlCommand();
            var id = "";

            using (cnn)
            {
                using (cmd)
                {
                   
                    cmd.Connection = cnn;
                    cnn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "P_NOTA_FISCAL";
                   
                    cmd.Parameters.AddWithValue("@pNumeroNotaFiscal", nota.NumeroNotaFiscal);
                    cmd.Parameters.AddWithValue("@pSerie", nota.Serie);
                    cmd.Parameters.AddWithValue("@pNomeCliente", nota.NomeCliente);
                    cmd.Parameters.AddWithValue("@pEstadoDestino", nota.EstadoDestino);
                    cmd.Parameters.AddWithValue("@pEstadoOrigem", nota.EstadoOrigem);

                    cmd.Parameters.AddWithValue("@pId", 0);



                    id = cmd.ExecuteScalar().ToString();
                   
               

                }
            }

            return id;
        }

        public void GravaProItens(Imposto.Core.Domain.NotaFiscalItem itens) 
        {
            var connectionString = ConfigurationManager.ConnectionStrings["mdl"].ConnectionString;
            var cnn = new SqlConnection();
            cnn.ConnectionString = connectionString;
            var cmd = new SqlCommand();
            using (cnn)
            {
                using (cmd)
                {
                    cmd.Connection = cnn;
                    cnn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "P_NOTA_FISCAL_ITEM";
                    cmd.Parameters.AddWithValue("@pId", 0);
                    cmd.Parameters.AddWithValue("@pIdNotaFiscal", itens.IdNotaFiscal);
                    cmd.Parameters.AddWithValue("@pCfop",itens.Cfop);
                    cmd.Parameters.AddWithValue("@pTipoIcms", itens.TipoIcms);
                    cmd.Parameters.AddWithValue("@pBaseIcms", itens.BaseIcms);
                    cmd.Parameters.AddWithValue("@pAliquotaIcms", itens.AliquotaIcms);
                    cmd.Parameters.AddWithValue("@pValorIcms", itens.ValorIcms);
                    cmd.Parameters.AddWithValue("@pNomeProduto", itens.NomeProduto);
                    cmd.Parameters.AddWithValue("@pCodigoProduto", itens.CodigoProduto);

                    cmd.Parameters.AddWithValue("@pBaseIpi", itens.BaseIpi);
                    cmd.Parameters.AddWithValue("@pAliquotaIpi", itens.AliquotaIpi);
                    cmd.Parameters.AddWithValue("@pValorIpi", itens.ValorIpi);
                    cmd.Parameters.AddWithValue("@desconto", itens.Desconto);



                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
