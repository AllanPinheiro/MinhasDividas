using Newtonsoft.Json; // Importa o namespace do Newtonsoft.Json para trabalhar com JSON

namespace MinhasDividas
{
    public partial class Form1 : Form
    {
        private List<Divida> dividas; // Lista para armazenar as d�vidas
        private string arquivoDados = "dados.json"; // Nome do arquivo JSON para salvar os dados
        private System.Windows.Forms.Timer timerStatusAdd; // Timer para limpar mensagem de status ap�s adi��o
        private System.Windows.Forms.Timer timerStatusDel; // Timer para limpar mensagem de status ap�s dele��o
        private System.Windows.Forms.Timer timerStatusEdit; // Timer para limpar mensagem de status ap�s edi��o

        public Form1()
        {
            InitializeComponent(); // Inicializa os componentes visuais do formul�rio
            LoadData(); // Carrega os dados do arquivo JSON (se existir)
            InitializeTimers(); // Inicializa os timers para as mensagens de status
        }

        private void InitializeTimers()
        {
            // Configura e inicializa o timer para a mensagem de status ap�s adicionar uma d�vida
            timerStatusAdd = new System.Windows.Forms.Timer { Interval = 2000 };
            timerStatusAdd.Tick += (s, e) =>
            {
                lblStatus.Text = string.Empty; // Limpa o texto da mensagem de status
                timerStatusAdd.Stop(); // Para o timer ap�s executar
            };

            // Configura e inicializa o timer para a mensagem de status ap�s deletar uma d�vida
            timerStatusDel = new System.Windows.Forms.Timer { Interval = 2000 };
            timerStatusDel.Tick += (s, e) =>
            {
                lblStatusDel.Text = string.Empty; // Limpa o texto da mensagem de status de dele��o
                timerStatusDel.Stop(); // Para o timer ap�s executar
            };

            // Configura e inicializa o timer para a mensagem de status ap�s editar uma d�vida
            timerStatusEdit = new System.Windows.Forms.Timer { Interval = 2000 };
            timerStatusEdit.Tick += (s, e) =>
            {
                lblStatusEditar.Text = string.Empty; // Limpa o texto da mensagem de status de edi��o
                timerStatusEdit.Stop(); // Para o timer ap�s executar
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string desc = txtDesc.Text; // Obt�m a descri��o da nova d�vida do TextBox
            string valor = txtValor.Text; // Obt�m o valor da nova d�vida do TextBox

            if (string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(valor))
            {
                // Verifica se os campos foram preenchidos
                lblStatus.Text = "Por favor, preencha todos os campos."; // Exibe mensagem de erro
                lblStatus.ForeColor = Color.Red; // Define a cor do texto como vermelho
                timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do m�todo
            }

            // Verifica se a descri��o j� existe na lista de d�vidas
            if (dividas.Any(d => d.Descricao == desc))
            {
                // Se j� existe, exibe mensagem de erro
                lblStatus.Text = "Item j� existe na lista."; // Exibe mensagem de erro
                lblStatus.ForeColor = Color.Red; // Define a cor do texto como vermelho
                txtDesc.Clear(); // Limpa o campo de descri��o
                txtValor.Clear(); // Limpa o campo de valor
                timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do m�todo
            }

            // Adiciona a nova d�vida � lista de d�vidas
            dividas.Add(new Divida { Descricao = desc, Valor = decimal.Parse(valor) });
            SaveData(); // Salva os dados atualizados

            lblStatus.Text = "Adicionado com sucesso!"; // Exibe mensagem de sucesso
            lblStatus.ForeColor = Color.Green; // Define a cor do texto como verde
            timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
            txtDesc.Clear(); // Limpa o campo de descri��o
            txtValor.Clear(); // Limpa o campo de valor
            LoadData(); // Atualiza a exibi��o na lista de d�vidas
        }

        private void LoadData()
        {
            // Carrega os dados do arquivo JSON se o arquivo existir
            if (File.Exists(arquivoDados))
            {
                string json = File.ReadAllText(arquivoDados); // L� todo o conte�do do arquivo JSON
                dividas = JsonConvert.DeserializeObject<List<Divida>>(json); // Deserializa o JSON para a lista de d�vidas
            }
            else
            {
                dividas = new List<Divida>(); // Se o arquivo n�o existir, inicializa uma nova lista de d�vidas
            }

            // Atualiza a exibi��o na ListBox e calcula o valor total das d�vidas
            lstItems.Items.Clear(); // Limpa os itens da ListBox
            decimal somaTotal = 0; // Vari�vel para somar o valor total das d�vidas

            foreach (var divida in dividas)
            {
                somaTotal += divida.Valor; // Soma o valor da d�vida ao total
                string valorFormatado = divida.Valor.ToString("C2"); // Formata o valor como moeda brasileira
                lstItems.Items.Add($"---- Descri��o: {divida.Descricao} ---- Valor R$: {valorFormatado} ----"); // Adiciona item formatado na ListBox
            }

            string somaTotalFormatada = somaTotal.ToString("C2"); // Formata o total das d�vidas como moeda brasileira
            lblSomaValor.Text = $"{somaTotalFormatada}"; // Exibe o total formatado na label
        }

        private void SaveData()
        {
            // Serializa a lista de d�vidas para JSON e salva no arquivo especificado
            string json = JsonConvert.SerializeObject(dividas); // Converte a lista de d�vidas para JSON
            File.WriteAllText(arquivoDados, json); // Escreve o JSON no arquivo
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string descDel = txtDel.Text; // Obt�m a descri��o da d�vida a ser deletada do TextBox

            if (string.IsNullOrEmpty(descDel))
            {
                // Verifica se o campo de descri��o para deletar est� vazio
                lblStatusDel.Text = "Preencha o campo deletar."; // Exibe mensagem de erro
                lblStatusDel.ForeColor = Color.Red; // Define a cor do texto como vermelho
                timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do m�todo
            }

            // Busca a d�vida pelo nome e remove da lista de d�vidas
            var divida = dividas.FirstOrDefault(d => d.Descricao == descDel);

            if (divida != null)
            {
                dividas.Remove(divida); // Remove a d�vida da lista de d�vidas
                SaveData(); // Salva os dados atualizados
                lblStatusDel.Text = "Deletado com sucesso!"; // Exibe mensagem de sucesso
                timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                lblStatusDel.ForeColor = Color.Green; // Define a cor do texto como verde
                txtDel.Clear(); // Limpa o campo de descri��o para deletar
                LoadData(); // Atualiza a exibi��o na lista de d�vidas
            }
            else
            {
                lblStatusDel.Text = "Item n�o existe!"; // Exibe mensagem de erro
                lblStatusDel.ForeColor = Color.Red; // Define a cor do texto como vermelho
                txtDel.Clear(); // Limpa o campo de descri��o para deletar
                timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string novaDesc = txtDescEdit.Text; // Obt�m a descri��o antiga da d�vida do TextBox
            string descEdit = txtNovaDescEdit.Text; // Obt�m a nova descri��o da d�vida do TextBox
            string valorEdit = txtNovoValorEdit.Text; // Obt�m o novo valor da d�vida do TextBox

            if (string.IsNullOrEmpty(descEdit) || string.IsNullOrEmpty(valorEdit))
            {
                // Verifica se os campos de edi��o est�o vazios
                lblStatusEditar.Text = "Preencha todos os campos para editar."; // Exibe mensagem de erro
                lblStatusEditar.ForeColor = Color.Red; // Define a cor do texto como vermelho
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do m�todo
            }

            // Verifica se a nova descri��o j� existe na lista de d�vidas
            if (dividas.Any(d => d.Descricao == descEdit))
            {
                // Se j� existe, exibe mensagem de erro
                lblStatusEditar.Text = "Item j� existe"; // Exibe mensagem de erro
                lblStatusEditar.ForeColor = Color.Red; // Define a cor do texto como vermelho
                txtDescEdit.Clear(); // Limpa o campo de descri��o antiga
                txtNovaDescEdit.Clear(); // Limpa o campo de nova descri��o
                txtNovoValorEdit.Clear(); // Limpa o campo de novo valor
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do m�todo
            }

            // Busca a d�vida pelo nome antigo e atualiza na lista de d�vidas
            var divida = dividas.FirstOrDefault(d => d.Descricao == novaDesc);

            if (divida != null)
            {
                divida.Descricao = descEdit; // Atualiza a descri��o da d�vida
                divida.Valor = decimal.Parse(valorEdit); // Atualiza o valor da d�vida
                SaveData(); // Salva os dados atualizados
                lblStatusEditar.Text = "Item editado com sucesso!"; // Exibe mensagem de sucesso
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                lblStatusEditar.ForeColor = Color.Green; // Define a cor do texto como verde
                txtDescEdit.Clear(); // Limpa o campo de descri��o antiga
                txtNovaDescEdit.Clear(); // Limpa o campo de nova descri��o
                txtNovoValorEdit.Clear(); // Limpa o campo de novo valor
                LoadData(); // Atualiza a exibi��o na lista de d�vidas
            }
            else
            {
                lblStatusEditar.Text = "Item n�o existe!"; // Exibe mensagem de erro
                lblStatusEditar.ForeColor = Color.Red; // Define a cor do texto como vermelho
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                txtDescEdit.Clear(); // Limpa o campo de descri��o antiga
                txtNovaDescEdit.Clear(); // Limpa o campo de nova descri��o
                txtNovoValorEdit.Clear(); // Limpa o campo de novo valor
            }
        }
    }

    // Classe para representar uma d�vida
    public class Divida
    {
        public string? Descricao { get; set; } // Propriedade para a descri��o da d�vida
        public decimal Valor { get; set; } // Propriedade para o valor da d�vida
    }
}
