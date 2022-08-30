using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;

namespace PrimeiroWebService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCep.Text))
            {
                using (var ws = new WSCorreios.AtendeClienteClient())
                    try
                    {
                        var consulta = ws.consultaCEP(txtCep.Text.Trim());
                        txtEstado.Text = consulta.uf;
                        txtRua.Text = consulta.end;
                        txtCidade.Text = consulta.cidade;
                        txtBairro.Text = consulta.bairro;

                        var teste = new Teste
                        {
                            Rua = consulta.end,
                            Cidade = consulta.cidade,
                            Cep = consulta.cep,
                            Estado = consulta.uf,
                            Bairro = consulta.bairro
                        };
                        string arquivo = @"c:\temp\teste.json";
                        FileStream criarArquivo = File.Create(arquivo);
                        JsonSerializer.SerializeAsync(criarArquivo, teste);
                        criarArquivo.Dispose();
                    }
                    catch (Exception erro)
                    {

                        MessageBox.Show(erro.Message);
                    }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtEstado.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtRua.Text = string.Empty;
            txtCep.Text = string.Empty;
        }
    }
}
