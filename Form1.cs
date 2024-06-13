using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MinhasDividas
{
    public partial class Form1 : Form
    {
        // Declaração dos timers para controlar as mensagens de status
        private System.Windows.Forms.Timer timerStatusAdd; // Timer para adição
        private System.Windows.Forms.Timer timerStatusDel; // Timer para exclusão
        private System.Windows.Forms.Timer timerStatusEdit; // Timer para edição

        public Form1()
        {
            InitializeComponent();
            LoadData(); // Carrega os dados ao iniciar o formulário
            InitializeTimers(); // Inicializa os timers
        }

        // Inicializa os timers e define seus eventos Tick
        private void InitializeTimers()
        {
            timerStatusAdd = new System.Windows.Forms.Timer { Interval = 2000 }; // Timer para adição de registros
            timerStatusAdd.Tick += (s, e) =>
            {
                lblStatus.Text = string.Empty; // Limpa o status após 2 segundos
                timerStatusAdd.Stop();
            };

            timerStatusDel = new System.Windows.Forms.Timer { Interval = 2000 }; // Timer para exclusão de registros
            timerStatusDel.Tick += (s, e) =>
            {
                lblStatusDel.Text = string.Empty; // Limpa o status após 2 segundos
                timerStatusDel.Stop();
            };

            timerStatusEdit = new System.Windows.Forms.Timer { Interval = 2000 }; // Timer para edição de registros
            timerStatusEdit.Tick += (s, e) =>
            {
                lblStatusEditar.Text = string.Empty; // Limpa o status após 2 segundos
                timerStatusEdit.Stop();
            };
        }

        // Método chamado quando o botão de adição é clicado
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string desc = txtDesc.Text; // Obtém a descrição do campo de texto
            string valor = txtValor.Text; // Obtém o valor do campo de texto

            // Verifica se os campos estão vazios e exibe uma mensagem caso estejam
            if (string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(valor))
            {
                lblStatus.Text = "Por favor, preencha todos os campos."; // Exibe mensagem de status
                timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
                return;
            }

            // Conexão com o banco de dados
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abre a conexão com o banco de dados

                    // Verifica se o item já existe no banco de dados
                    string checkQuery = "SELECT COUNT(1) FROM DIVIDAS WHERE DESCRICAO = @DESCRICAO";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@DESCRICAO", desc); // Adiciona parâmetro
                        int count = (int)checkCommand.ExecuteScalar(); // Executa a consulta e obtém o resultado
                        if (count > 0)
                        {
                            lblStatus.Text = "Item já existe no banco de dados."; // Exibe mensagem de status
                            timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
                            return;
                        }
                    }

                    // Insere um novo registro no banco de dados
                    string insertQuery = "INSERT INTO DIVIDAS (DESCRICAO, VALOR) VALUES (@DESCRICAO, @VALOR)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@DESCRICAO", desc); // Adiciona parâmetro
                        cmd.Parameters.AddWithValue("@VALOR", valor); // Adiciona parâmetro

                        int rowsAffected = cmd.ExecuteNonQuery(); // Executa a inserção e obtém o número de linhas afetadas
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

        // Método para carregar os dados na lista ao iniciar o formulário e após adições, exclusões ou edições
        private void LoadData()
        {
            // Conexão com o banco de dados
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abre a conexão com o banco de dados
                    string query = "SELECT ID, DESCRICAO, VALOR FROM DIVIDAS ORDER BY ID ASC;"; // Consulta SQL
                    SqlDataAdapter da = new SqlDataAdapter(query, connection); // Cria um adaptador de dados
                    DataTable dt = new DataTable(); // Cria uma tabela de dados
                    da.Fill(dt); // Preenche a tabela com os dados obtidos pela consulta
                    lstItems.Items.Clear(); // Limpa a lista de itens
                    decimal somaTotal = 0; // Variável para armazenar a soma total dos valores

                    foreach (DataRow row in dt.Rows) // Loop para cada linha da tabela de dados
                    {
                        int id = Convert.ToInt32(row["ID"]); // Obtém o ID do registro
                        decimal valor = Convert.ToDecimal(row["VALOR"]); // Obtém o valor do registro
                        somaTotal += valor; // Adiciona o valor à soma total
                        string valorFormatado = valor.ToString("C2", new System.Globalization.CultureInfo("pt-BR")); // Formata o valor para exibição
                        lstItems.Items.Add($"---- ID: {id} ---- Descrição: {row["DESCRICAO"]} ---- Valor R$: {valorFormatado} ----"); // Adiciona o item à lista
                    }

                    string somaTotalFormatada = somaTotal.ToString("C2", new System.Globalization.CultureInfo("pt-BR")); // Formata a soma total para exibição
                    lblSomaValor.Text = $"{somaTotalFormatada}"; // Exibe a soma total no label
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar os dados: " + ex.Message); // Exibe mensagem de erro
                }
            }
        }

        // Método chamado quando o botão de exclusão é clicado
        private void btnDel_Click(object sender, EventArgs e)
        {
            string descDel = txtDel.Text; // Obtém a descrição do campo de texto

            // Verifica se o campo está vazio e exibe uma mensagem caso esteja
            if (string.IsNullOrEmpty(descDel))
            {
                lblStatusDel.Text = "Preencha o campo deletar."; // Exibe mensagem de status
                timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                return;
            }

            // Conexão com o banco de dados
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abre a conexão com o banco de dados
                    string query = "DELETE FROM DIVIDAS WHERE DESCRICAO = @DESCRICAO"; // Comando SQL para deletar um registro
                    SqlCommand cmd = new SqlCommand(query, connection); // Cria um comando SQL
                    cmd.Parameters.AddWithValue("@DESCRICAO", descDel); // Adiciona parâmetro ao comando SQL

                    int rowsAffected = cmd.ExecuteNonQuery(); // Executa o comando SQL e obtém o número de linhas afetadas
                    if (rowsAffected > 0)
                    {
                        lblStatusDel.Text = "Deletado com sucesso!"; // Exibe mensagem de status
                        timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                        txtDel.Clear(); // Limpa o campo de texto
                        LoadData(); // Recarrega os dados na lista
                    }
                    else
                    {
                        lblStatusDel.Text = "Item não existe!"; // Exibe mensagem de status
                        timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar os dados: " + ex.Message); // Exibe mensagem de erro
                }
            }
        }

        // Método chamado quando o botão de edição é clicado
        private void btnEditar_Click(object sender, EventArgs e)
        {
            string id = txtIdEdit.Text; // Obtém o ID do campo de texto para edição
            string descEdit = txtDescEdit.Text; // Obtém a nova descrição do campo de texto para edição
            string valorEdit = txtValorEdit.Text; // Obtém o novo valor do campo de texto para edição

            // Verifica se os campos de edição estão vazios e exibe uma mensagem caso estejam
            if (string.IsNullOrEmpty(descEdit) || string.IsNullOrEmpty(valorEdit))
            {
                lblStatusEditar.Text = "Preencha todos os campos para editar."; // Exibe mensagem de status
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                return;
            }

            // Conexão com o banco de dados
            string connectionString = "Data Source=DESKTOP-QRDBJEB\\SQLEXPRESS;Initial Catalog=DBMINHASDIVIDA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abre a conexão com o banco de dados
                    string query = "UPDATE DIVIDAS SET DESCRICAO = @DESCRICAO, VALOR = @VALOR WHERE ID = @ID"; // Comando SQL para atualizar um registro
                    SqlCommand cmd = new SqlCommand(query, connection); // Cria um comando SQL
                    cmd.Parameters.AddWithValue("@DESCRICAO", descEdit); // Adiciona parâmetro ao comando SQL para a nova descrição
                    cmd.Parameters.AddWithValue("@VALOR", valorEdit); // Adiciona parâmetro ao comando SQL para o novo valor
                    cmd.Parameters.AddWithValue("@ID", id); // Adiciona parâmetro ao comando SQL para o ID do registro a ser editado

                    int rowsAffected = cmd.ExecuteNonQuery(); // Executa o comando SQL e obtém o número de linhas afetadas
                    if (rowsAffected > 0)
                    {
                        lblStatusEditar.Text = "Item editado com sucesso!"; // Exibe mensagem de status
                        timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                        txtIdEdit.Clear(); // Limpa o campo de texto do ID
                        txtDescEdit.Clear(); // Limpa o campo de texto da descrição
                        txtValorEdit.Clear(); // Limpa o campo de texto do valor
                        LoadData(); // Recarrega os dados na lista
                    }
                    else
                    {
                        lblStatusEditar.Text = "Item não existe!"; // Exibe mensagem de status
                        timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                        txtIdEdit.Clear(); // Limpa o campo de texto do ID
                        txtDescEdit.Clear(); // Limpa o campo de texto da descrição
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


