using Microsoft.Data.SqlClient;

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
    }
}
