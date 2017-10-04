using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Core.Domain;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();

        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
            CboEstadoDestino();
            ListaDeEstadosOrigem();
        }

        public class Estados
        {
            public string valor { get; set; }
            public string Desc { get; set; }
            public Estados(string valor, string Desc) {
                this.valor = valor;
                this.Desc = Desc;
            }
        }
        public void ListaDeEstadosOrigem()
        {
            var listaEstados = new System.Collections.ArrayList();
            listaEstados.Add(new Estados("SP", "SP"));
            listaEstados.Add(new Estados("MG", "MG"));
            cboEstadoOrigem.DataSource = listaEstados;
            cboEstadoOrigem.DisplayMember = "Desc";
            cboEstadoOrigem.ValueMember = "valor";

            cboEstadoOrigem.DropDownStyle = ComboBoxStyle.DropDownList;


        }
        public void CboEstadoDestino()
        {
            var listaEstados = new System.Collections.ArrayList();
            listaEstados.Add(new Estados("RJ", "RJ"));
            listaEstados.Add(new Estados("PE", "PE"));
            listaEstados.Add(new Estados("PE", "PE"));
            listaEstados.Add(new Estados("MG", "MG"));
            listaEstados.Add(new Estados("PB", "PB"));
            listaEstados.Add(new Estados("PR", "PR"));
            listaEstados.Add(new Estados("PI", "PI"));
            listaEstados.Add(new Estados("RO", "RO"));
            listaEstados.Add(new Estados("SE", "SE"));
            listaEstados.Add(new Estados("TO", "TO"));
            listaEstados.Add(new Estados("PA", "PA"));
            cboEstadoDestino.DataSource = listaEstados;
            cboEstadoDestino.DisplayMember = "Desc";
            cboEstadoDestino.ValueMember = "valor";

            cboEstadoDestino.DropDownStyle = ComboBoxStyle.DropDownList;

        }
        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {            
            NotaFiscalService service = new NotaFiscalService();
            pedido.EstadoOrigem = cboEstadoOrigem.SelectedValue.ToString() ;
            pedido.EstadoDestino = cboEstadoDestino.SelectedValue.ToString();
            pedido.NomeCliente = textBoxNomeCliente.Text;

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            foreach (DataRow row in table.Rows)
            {
                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = Convert.ToBoolean(row["Brinde"]),
                        CodigoProduto =  row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())            
                    });
            }

           var nota = service.GerarNotaFiscal(pedido);
           var xml = service.GerarXml(nota);

            MessageBox.Show("Operação efetuada com sucesso");

            textBoxNomeCliente.Text = "";
            dataGridViewPedidos.AutoGenerateColumns = true;
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
            CboEstadoDestino();
            ListaDeEstadosOrigem();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormImposto_Load(object sender, EventArgs e)
        {

        }

        private void cboEstadoOrigem_ValueMemberChanged(object sender, EventArgs e)
        {

        }
    }
}
