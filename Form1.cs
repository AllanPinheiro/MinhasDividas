using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MinhasDividas
{
    public partial class Form1 : Form
    {
        // Declara��o dos timers para controlar as mensagens de status
        private System.Windows.Forms.Timer timerStatusAdd; // Timer para adi��o
        private System.Windows.Forms.Timer timerStatusDel; // Timer para exclus�o
        private System.Windows.Forms.Timer timerStatusEdit; // Timer para edi��o

        public Form1()
        {
            InitializeComponent();
            LoadData(); // Carrega os dados ao iniciar o formul�rio
            InitializeTimers(); // Inicializa os timers
        }

        // Inicializa os timers e define seus eventos Tick
        private void InitializeTimers()
        {
            timerStatusAdd = new System.Windows.Forms.Timer { Interval = 2000 }; // Timer para adi��o de registros
            timerStatusAdd.Tick += (s, e) =>
            {
                lblStatus.Text = string.Empty; // Limpa o status ap�s 2 segundos
                timerStatusAdd.Stop();
            };

            timerStatusDel = new System.Windows.Forms.Timer { Interval = 2000 }; // Timer para exclus�o de registros
            timerStatusDel.Tick += (s, e) =>
            {
                lblStatusDel.Text = string.Empty; // Limpa o status ap�s 2 segundos
                timerStatusDel.Stop();
            };

            timerStatusEdit = new System.Windows.Forms.Timer { Interval = 2000 }; // Timer para edi��o de registros
            timerStatusEdit.Tick += (s, e) =>
            {
                lblStatusEditar.Text = string.Empty; // Limpa o status ap�s 2 segundos
                timerStatusEdit.Stop();
            };
        }

        // M�todo chamado quando o bot�o de adi��o � clicado
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string desc = txtDesc.Text; // Obt�m a descri��o do campo de texto
            string valor = txtValor.Text; // Obt�m o valor do campo de texto

            // Verifica se os campos est�o vazios e exibe uma mensagem caso estejam
            if (string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(valor))
            {
                lblStatus.Text = "Por favor, preencha todos os campos."; // Exibe mensagem de status
                timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
                return;
            }

            // Conex�o com o banco de dados
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abre a conex�o com o banco de dados

                    // Verifica se o item j� existe no banco de dados
                    string checkQuery = "SELECT COUNT(1) FROM DIVIDAS WHERE DESCRICAO = @DESCRICAO";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@DESCRICAO", desc); // Adiciona par�metro
                        int count = (int)checkCommand.ExecuteScalar(); // Executa a consulta e obt�m o resultado
                        if (count > 0)
                        {
                            lblStatus.Text = "Item j� existe no banco de dados."; // Exibe mensagem de status
                            timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
                            return;
                        }
                    }

                    // Insere um novo registro no banco de dados
                    string insertQuery = "INSERT INTO DIVIDAS (DESCRICAO, VALOR) VALUES (@DESCRICAO, @VALOR)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@DESCRICAO", desc); // Adiciona par�metro
                        cmd.Parameters.AddWithValue("@VALOR", valor); // Adiciona par�metro

                        int rowsAffected = cmd.ExecuteNonQuery(); // Executa a inser��o e obt�m o n�mero de linhas afetadas
                        if (rowsAffected > 0)
                        {
                            lblStatus.Text = "Adicionado com sucesso!"; // Exibe mensagem de status
                            timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
                            txtDesc.Clear(); // Limpa o campo de texto
                            txtValor.Clear(); // Limpa o campo de texto
                            LoadData(); // Recarrega os dados na lista
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar os dados: " + ex.Message); // Exibe mensagem de erro
                }
            }
        }

        // M�todo para carregar os dados na lista ao iniciar o formul�rio e ap�s adi��es, exclus�es ou edi��es
        private void LoadData()
        {
            // Conex�o com o banco de dados
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abre a conex�o com o banco de dados
                    string query = "SELECT ID, DESCRICAO, VALOR FROM DIVIDAS ORDER BY ID ASC;"; // Consulta SQL
                    SqlDataAdapter da = new SqlDataAdapter(query, connection); // Cria um adaptador de dados
                    DataTable dt = new DataTable(); // Cria uma tabela de dados
                    da.Fill(dt); // Preenche a tabela com os dados obtidos pela consulta
                    lstItems.Items.Clear(); // Limpa a lista de itens
                    decimal somaTotal = 0; // Vari�vel para armazenar a soma total dos valores

                    foreach (DataRow row in dt.Rows) // Loop para cada linha da tabela de dados
                    {
                        int id = Convert.ToInt32(row["ID"]); // Obt�m o ID do registro
                        decimal valor = Convert.ToDecimal(row["VALOR"]); // Obt�m o valor do registro
                        somaTotal += valor; // Adiciona o valor � soma total
                        string valorFormatado = valor.ToString("C2", new System.Globalization.CultureInfo("pt-BR")); // Formata o valor para exibi��o
                        lstItems.Items.Add($"---- ID: {id} ---- Descri��o: {row["DESCRICAO"]} ---- Valor R$: {valorFormatado} ----"); // Adiciona o item � lista
                    }

                    string somaTotalFormatada = somaTotal.ToString("C2", new System.Globalization.CultureInfo("pt-BR")); // Formata a soma total para exibi��o
                    lblSomaValor.Text = $"{somaTotalFormatada}"; // Exibe a soma total no label
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar os dados: " + ex.Message); // Exibe mensagem de erro
                }
            }
        }

        // M�todo chamado quando o bot�o de exclus�o � clicado
        private void btnDel_Click(object sender, EventArgs e)
        {
            string descDel = txtDel.Text; // Obt�m a descri��o do campo de texto

            // Verifica se o campo est� vazio e exibe uma mensagem caso esteja
            if (string.IsNullOrEmpty(descDel))
            {
                lblStatusDel.Text = "Preencha o campo deletar."; // Exibe mensagem de status
                timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                return;
            }

            // Conex�o com o banco de dados
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abre a conex�o com o banco de dados
                    string query = "DELETE FROM DIVIDAS WHERE DESCRICAO = @DESCRICAO"; // Comando SQL para deletar um registro
                    SqlCommand cmd = new SqlCommand(query, connection); // Cria um comando SQL
                    cmd.Parameters.AddWithValue("@DESCRICAO", descDel); // Adiciona par�metro ao comando SQL

                    int rowsAffected = cmd.ExecuteNonQuery(); // Executa o comando SQL e obt�m o n�mero de linhas afetadas
                    if (rowsAffected > 0)
                    {
                        lblStatusDel.Text = "Deletado com sucesso!"; // Exibe mensagem de status
                        timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                        txtDel.Clear(); // Limpa o campo de texto
                        LoadData(); // Recarrega os dados na lista
                    }
                    else
                    {
                        lblStatusDel.Text = "Item n�o existe!"; // Exibe mensagem de status
                        timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar os dados: " + ex.Message); // Exibe mensagem de erro
                }
            }
        }

        // M�todo chamado quando o bot�o de edi��o � clicado
        private void btnEditar_Click(object sender, EventArgs e)
        {
            string id = txtIdEdit.Text; // Obt�m o ID do campo de texto para edi��o
            string descEdit = txtDescEdit.Text; // Obt�m a nova descri��o do campo de texto para edi��o
            string valorEdit = txtValorEdit.Text; // Obt�m o novo valor do campo de texto para edi��o

            // Verifica se os campos de edi��o est�o vazios e exibe uma mensagem caso estejam
            if (string.IsNullOrEmpty(descEdit) || string.IsNullOrEmpty(valorEdit))
            {
                lblStatusEditar.Text = "Preencha todos os campos para editar."; // Exibe mensagem de status
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                return;
            }

            // Conex�o com o banco de dados
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abre a conex�o com o banco de dados
                    string query = "UPDATE DIVIDAS SET DESCRICAO = @DESCRICAO, VALOR = @VALOR WHERE ID = @ID"; // Comando SQL para atualizar um registro
                    SqlCommand cmd = new SqlCommand(query, connection); // Cria um comando SQL
                    cmd.Parameters.AddWithValue("@DESCRICAO", descEdit); // Adiciona par�metro ao comando SQL para a nova descri��o
                    cmd.Parameters.AddWithValue("@VALOR", valorEdit); // Adiciona par�metro ao comando SQL para o novo valor
                    cmd.Parameters.AddWithValue("@ID", id); // Adiciona par�metro ao comando SQL para o ID do registro a ser editado

                    int rowsAffected = cmd.ExecuteNonQuery(); // Executa o comando SQL e obt�m o n�mero de linhas afetadas
                    if (rowsAffected > 0)
                    {
                        lblStatusEditar.Text = "Item editado com sucesso!"; // Exibe mensagem de status
                        timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                        txtIdEdit.Clear(); // Limpa o campo de texto do ID
                        txtDescEdit.Clear(); // Limpa o campo de texto da descri��o
                        txtValorEdit.Clear(); // Limpa o campo de texto do valor
                        LoadData(); // Recarrega os dados na lista
                    }
                    else
                    {
                        lblStatusEditar.Text = "Item n�o existe!"; // Exibe mensagem de status
                        timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                        txtIdEdit.Clear(); // Limpa o campo de texto do ID
                        txtDescEdit.Clear(); // Limpa o campo de texto da descri��o
                        txtValorEdit.Clear(); // Limpa o campo de texto do valor
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao editar os dados: " + ex.Message); // Exibe mensagem de erro
                }
            }
        }
    }
}


