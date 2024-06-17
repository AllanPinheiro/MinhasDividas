using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MinhasDividas
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timerStatusAdd;
        private System.Windows.Forms.Timer timerStatusDel;
        private System.Windows.Forms.Timer timerStatusEdit;

        public Form1()
        {
            InitializeComponent();
            LoadData();
            InitializeTimers();
        }

        private void InitializeTimers()
        {
            timerStatusAdd = new System.Windows.Forms.Timer { Interval = 2000 };
            timerStatusAdd.Tick += (s, e) =>
            {
                lblStatus.Text = string.Empty;
                timerStatusAdd.Stop();
            };

            timerStatusDel = new System.Windows.Forms.Timer { Interval = 2000 };
            timerStatusDel.Tick += (s, e) =>
            {
                lblStatusDel.Text = string.Empty;
                timerStatusDel.Stop();
            };

            timerStatusEdit = new System.Windows.Forms.Timer { Interval = 2000 };
            timerStatusEdit.Tick += (s, e) =>
            {
                lblStatusEditar.Text = string.Empty;
                timerStatusEdit.Stop();
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string desc = txtDesc.Text;
            string valor = txtValor.Text;

            if (string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(valor))
            {
                lblStatus.Text = "Por favor, preencha todos os campos.";
                lblStatus.ForeColor = Color.Red;
                timerStatusAdd.Start();
                return;
            }

            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(1) FROM DIVIDAS WHERE DESCRICAO = @DESCRICAO";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@DESCRICAO", desc);
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            lblStatus.Text = "Item já existe no banco de dados.";
                            lblStatus.ForeColor = Color.Red;
                            txtDesc.Clear();
                            txtValor.Clear();
                            timerStatusAdd.Start();
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO DIVIDAS (DESCRICAO, VALOR) VALUES (@DESCRICAO, @VALOR)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@DESCRICAO", desc);
                        cmd.Parameters.AddWithValue("@VALOR", valor);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblStatus.Text = "Adicionado com sucesso!";
                            lblStatus.ForeColor = Color.Green;
                            timerStatusAdd.Start();
                            txtDesc.Clear();
                            txtValor.Clear();
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar os dados: " + ex.Message);
                }
            }
        }

        private void LoadData()
        {
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT DESCRICAO, VALOR FROM DIVIDAS;";
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lstItems.Items.Clear();
                    decimal somaTotal = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        decimal valor = Convert.ToDecimal(row["VALOR"]);
                        somaTotal += valor;
                        string valorFormatado = valor.ToString("C2", new System.Globalization.CultureInfo("pt-BR"));
                        lstItems.Items.Add($"---- Descrição: {row["DESCRICAO"]} ---- Valor R$: {valorFormatado} ----");
                    }

                    string somaTotalFormatada = somaTotal.ToString("C2", new System.Globalization.CultureInfo("pt-BR"));
                    lblSomaValor.Text = $"{somaTotalFormatada}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar os dados: " + ex.Message);
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string descDel = txtDel.Text;

            if (string.IsNullOrEmpty(descDel))
            {
                lblStatusDel.Text = "Preencha o campo deletar.";
                lblStatusDel.ForeColor = Color.Red;
                timerStatusDel.Start();
                return;
            }

            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM DIVIDAS WHERE DESCRICAO = @DESCRICAO";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@DESCRICAO", descDel);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblStatusDel.Text = "Deletado com sucesso!";
                        lblStatusDel.ForeColor = Color.Green;
                        timerStatusDel.Start();
                        txtDel.Clear();
                        LoadData();
                    }
                    else
                    {
                        lblStatusDel.Text = "Item não existe!";
                        lblStatusDel.ForeColor = Color.Red;
                        txtDel.Clear();
                        timerStatusDel.Start();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar os dados: " + ex.Message);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string novaDesc = txtDescEdit.Text;
            string descEdit = txtNovaDescEdit.Text;
            string valorEdit = txtNovoValorEdit.Text;

            if (string.IsNullOrEmpty(descEdit) || string.IsNullOrEmpty(valorEdit))
            {
                lblStatusEditar.Text = "Preencha todos os campos para editar.";
                lblStatusEditar.ForeColor = Color.Red;
                timerStatusEdit.Start();
                return;
            }

            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string checkQuery = "SELECT COUNT(1) FROM DIVIDAS WHERE DESCRICAO = @DESCRICAO";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        connection.Open();
                        checkCommand.Parameters.AddWithValue("@DESCRICAO", descEdit);
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            lblStatusEditar.Text = "Item já existe";
                            lblStatusEditar.ForeColor = Color.Red;
                            txtDescEdit.Clear();
                            txtNovaDescEdit.Clear();
                            txtNovoValorEdit.Clear();
                            timerStatusEdit.Start();
                            return;
                        }

                        string query = "UPDATE DIVIDAS SET DESCRICAO = @DESCRICAO, VALOR = @VALOR WHERE DESCRICAO = @NOVADESCRICAO";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@DESCRICAO", descEdit);
                        cmd.Parameters.AddWithValue("@VALOR", valorEdit);
                        cmd.Parameters.AddWithValue("@NOVADESCRICAO", novaDesc);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblStatusEditar.Text = "Item editado com sucesso!";
                            lblStatusEditar.ForeColor = Color.Green;
                            timerStatusEdit.Start();
                            txtDescEdit.Clear();
                            txtNovaDescEdit.Clear();
                            txtNovoValorEdit.Clear();
                            LoadData();
                        }
                        else
                        {
                            lblStatusEditar.Text = "Item não existe!";
                            lblStatusEditar.ForeColor = Color.Red;
                            timerStatusEdit.Start();
                            txtDescEdit.Clear();
                            txtNovaDescEdit.Clear();
                            txtNovoValorEdit.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao editar os dados: " + ex.Message);
                }
            }
        }
    }
}