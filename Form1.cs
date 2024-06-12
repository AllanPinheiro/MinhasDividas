using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MinhasDividas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
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
                return;
            }
            else if (string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(valor))
            {
                lblStatus.Text = "Por favor, preencha todos os campos.";
                return;
            }

            // CONEXAO COM BANCO DE DADOS
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // METODO INSERT INTO NO BANCO DE DADOS
                    string query = "INSERT INTO DIVIDAS (DESCRICAO, VALOR) VALUES (@DESCRICAO, @VALOR)";
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
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar os dados: " + ex.Message);
                }
            }
        }

        // LOAD DE ITEM ADICIONADO
        private void LoadData()
        {
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ID, DESCRICAO, VALOR FROM DIVIDAS ORDER BY VALOR DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lstItems.Items.Clear();
                    decimal somaTotal = 0; // Vari�vel para acumular a soma dos valores

                    foreach (DataRow row in dt.Rows)
                    {
                        int id = Convert.ToInt32(row["ID"]); // Obt�m o ID da linha
                        decimal valor = Convert.ToDecimal(row["VALOR"]);
                        somaTotal += valor; // Adiciona o valor atual � soma total
                        string valorFormatado = valor.ToString("C2", new System.Globalization.CultureInfo("pt-BR"));
                        lstItems.Items.Add($"---- ID: {id} ----- Descri��o: {row["DESCRICAO"]} ----- Valor R$: {valorFormatado} -----");
                    }
                    // Formatar e exibir a soma total
                    string somaTotalFormatada = somaTotal.ToString("C2", new System.Globalization.CultureInfo("pt-BR"));
                    lblSomaValor.Text = $"{somaTotalFormatada}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar os dados: " + ex.Message);
                }
            }
        }

        // BOTAO DELETAR
        private void btnDel_Click(object sender, EventArgs e)
        {
            // VERIFICA CAMPO SE ESTAO VAZIO
            string descDel = txtDel.Text;
            if (string.IsNullOrEmpty(descDel))
            {
                lblStatusDel.Text = "Preencha o campo deletar.";
                return;
            }
            else
            {
                lblStatusDel.ResetText();
                txtDel.ResetText();
            }

            // CONEXAO COM BANCO DE DADOS
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // METODO INSERT INTO NO BANCO DE DADOS
                    string query = "DELETE FROM DIVIDAS WHERE DESCRICAO = @DESCRICAO";
                    SqlCommand cmd = new SqlCommand(query, connection);

                    // EVITAR SQL INJECTION
                    cmd.Parameters.AddWithValue("@DESCRICAO", descDel);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblStatusDel.Text = "Deletado com sucesso!";
                        txtDel.ResetText();
                        LoadData();
                    }
                    else
                    {
                        lblStatusDel.Text = "Item n�o existe!";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar os dados: " + ex.Message);
                }
            }
        }

        // BOTAO EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // VERIFICA CAMPOS SE ESTAO VAZIO
            string id = txtIdEdit.Text;
            string descEdit = txtDescEdit.Text;
            string valorEdit = txtValorEdit.Text;
            if (string.IsNullOrEmpty(descEdit) && string.IsNullOrEmpty(valorEdit))
            {
                lblStatusEditar.Text = "Preencha os campos editar.";
                return;
            }
            else if (string.IsNullOrEmpty(descEdit) || string.IsNullOrEmpty(valorEdit))
            {
                lblStatusEditar.Text = "Preencha todos os campos editar.";
                return;
            }
            else
            {
                lblStatusEditar.ResetText();
                txtDescEdit.ResetText();
                txtValorEdit.ResetText();
            }

            // CONEXAO COM BANCO DE DADOS
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // METODO INSERT INTO NO BANCO DE DADOS
                    string query = "UPDATE DIVIDAS SET DESCRICAO = @DESCRICAO, VALOR = @VALOR WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    // EVITAR SQL INJECTION
                    cmd.Parameters.AddWithValue("@DESCRICAO", descEdit);
                    cmd.Parameters.AddWithValue("@VALOR", valorEdit);
                    cmd.Parameters.AddWithValue("@ID", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblStatusEditar.Text = "Item editado com sucesso!";
                        txtIdEdit.ResetText();
                        txtDescEdit.ResetText();
                        txtValorEdit.ResetText();
                        LoadData();
                    }
                    else
                    {
                        lblStatusEditar.Text = "Item n�o existe!";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar os dados: " + ex.Message);
                }
            }
        }
    }
}
