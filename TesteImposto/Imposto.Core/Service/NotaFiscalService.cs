using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text;
using System.IO;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public Imposto.Core.Domain.NotaFiscal GerarNotaFiscal(Domain.Pedido pedido)
        {
            Imposto.Core.Domain.NotaFiscal notaFiscal = new Imposto.Core.Domain.NotaFiscal();
           var itens =  notaFiscal.EmitirNotaFiscal(pedido);

           notaFiscal.ItensDaNotaFiscal = itens;
           return notaFiscal;
        }

        public string GerarXml(Imposto.Core.Domain.NotaFiscal nota)
        {
            var numNota = nota.NumeroNotaFiscal.ToString();
            var ret = "";
            var patch = @"C:\tmp";
            if (!Directory.Exists(patch))
            {
                Directory.CreateDirectory(patch);
            }
 


            try
            {

                XmlTextWriter writer = new XmlTextWriter(patch +"\\"+ numNota + ".xml", null);
                writer.WriteStartDocument();
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("Nota Fiscal_" + numNota + "");
                //Escreve os sub-elementos
                writer.WriteElementString("Numero da Nota", nota.NumeroNotaFiscal.ToString());
                writer.WriteElementString("Serie", nota.Serie.ToString());
                writer.WriteElementString("Nome Cliente", nota.NomeCliente.ToString());
                writer.WriteElementString("EstadoDestino", nota.EstadoDestino.ToString());
                writer.WriteElementString("EstadoOrigem", nota.EstadoOrigem.ToString());
                writer.WriteStartElement("ItensDaNotaFiscal", "Quantidade de Itens " + nota.ItensDaNotaFiscal.Count().ToString());

                foreach (var item in nota.ItensDaNotaFiscal)
                {
                    if (item.Cfop != null)
                    {
                        writer.WriteElementString("Cpof", item.Cfop.ToString());
                    }
                    else
                    {
                        writer.WriteElementString("Cpof", "0");
                    }
                  
                    writer.WriteElementString("TipoIcms", item.TipoIcms.ToString());
                    writer.WriteElementString("BaseIcms", item.BaseIcms.ToString());
                    writer.WriteElementString("AliquotaIcms", item.AliquotaIcms.ToString());
                    writer.WriteElementString("ValorIcms", item.ValorIcms.ToString());
                    writer.WriteElementString("NomeProduto", item.NomeProduto.ToString());
                    writer.WriteElementString("CodigoProduto", item.CodigoProduto.ToString());

                    writer.WriteElementString("Base Ipi", item.BaseIpi.ToString());
                    writer.WriteElementString("AliquotaIpi", item.AliquotaIpi.ToString());
                    writer.WriteElementString("Valor Ipi", item.ValorIpi.ToString());
                    writer.WriteElementString("Desconto", item.Desconto.ToString());



                    //notaFiscalItem.BaseIpi = itemPedido.ValorItemPedido;
                    //notaFiscalItem.AliquotaIpi = itemPedido.Brinde == true ? 0 : 10;
                    //notaFiscalItem.ValorIpi = notaFiscalItem.AliquotaIpi * notaFiscalItem.BaseIpi;

                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                
                writer.Close();
               
               
                var repo = new Imposto.Core.Data.NotaFiscalRepository();
                try
                {
                    var itens = new Imposto.Core.Domain.NotaFiscalItem();
                   var id = repo.GravaDadosNotaFiscal(nota);
                    foreach (var item in nota.ItensDaNotaFiscal)
                    {
                        itens.AliquotaIcms = item.AliquotaIcms;
                        itens.BaseIcms = item.BaseIcms;
                        itens.Cfop = item.Cfop;
                        itens.CodigoProduto = item.CodigoProduto;
                        itens.IdNotaFiscal = int.Parse(id);
                        itens.NomeProduto = item.NomeProduto;
                        itens.TipoIcms = item.TipoIcms;
                        itens.ValorIcms = item.ValorIcms;
                        repo.GravaProItens(itens);
                    }
                    
                }
                catch (Exception ex)
                {
                    
                    ret = ex.Message;
                }
                ret = "1";
                


            }
            catch (Exception ex)
            {
                ret = ex.ToString();
                throw;
            }

            return ret;
        }
    }
}
