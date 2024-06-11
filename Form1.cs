using Microsoft.Data.SqlClient;
using System.Drawing;

namespace MinhasDividas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // BOTAO ADICIONAR
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // VERIFICA CAMPOS SE ESTAO VAZIO
            string desc = txtDesc.Text;
            string valor = txtValor.Text;
            if (string.IsNullOrEmpty(desc) && string.IsNullOrEmpty(valor))
            {
                lblStatus.Text = "Por favor, preencha os campos.";
            }
            else if (string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(valor))
            {
                lblStatus.Text = "Por favor, preencha todos os campos.";
            }
            else
            {
                lblStatus.ResetText();
                txtDesc.ResetText();
                txtValor.ResetText();
            }
            // CONEXAO COM BANCO DE DADOS
            /*string connectionString = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // METODO INSERT INTO NO BANCO DE DADOS
                    string query = "INSERT INTO DIVIDA (DESCRICAO, VALOR) VALUES (@DESCRICAO, @VALOR)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    // EVITAR SQL INJECTION
                    cmd.Parameters.AddWithValue("@DESCRICAO", desc);
                    cmd.Parameters.AddWithValue("@VALOR", valor);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblStatus.Text = "Adicionado com sucesso!";
                        txtDesc.ResetText();
                        txtValor.ResetText();
                    }
                    else
                    {
                        lblStatus.Text = "Erro ao adicionar";
                        txtDesc.ResetText();
                        txtValor.ResetText();
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = ex.Message;
                }
            }*/
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string descDel = txtDel.Text;
            if (string.IsNullOrEmpty(descDel))
            {
                lblStatusDel.Text = "Preencha o campo deletar.";
            }
            else
            {
                lblStatusDel.ResetText();
                txtDel.ResetText();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string descEdit = txtDescEdit.Text;
            string valorEdit = txtValorEdit.Text;
            if (string.IsNullOrEmpty(descEdit) && string.IsNullOrEmpty(valorEdit))
            {
                lblStatusEditar.Text = "Preencha os campos editar.";
            }
            else if (string.IsNullOrEmpty(descEdit) || string.IsNullOrEmpty(valorEdit))
            {
                lblStatusEditar.Text = "Preencha todos os campos editar.";
            }
            else
            {
                lblStatusEditar.ResetText();
                txtDescEdit.ResetText();
                txtValorEdit.ResetText();
            }
        }
    }
}
