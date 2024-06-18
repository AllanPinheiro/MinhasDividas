using Newtonsoft.Json; // Importa o namespace do Newtonsoft.Json para trabalhar com JSON

namespace MinhasDividas
{
    public partial class Form1 : Form
    {
        private List<Divida> dividas; // Lista para armazenar as dívidas
        private string arquivoDados = "dados.json"; // Nome do arquivo JSON para salvar os dados
        private System.Windows.Forms.Timer timerStatusAdd; // Timer para limpar mensagem de status após adição
        private System.Windows.Forms.Timer timerStatusDel; // Timer para limpar mensagem de status após deleção
        private System.Windows.Forms.Timer timerStatusEdit; // Timer para limpar mensagem de status após edição

        public Form1()
        {
            InitializeComponent(); // Inicializa os componentes visuais do formulário
            LoadData(); // Carrega os dados do arquivo JSON (se existir)
            InitializeTimers(); // Inicializa os timers para as mensagens de status
        }

        private void InitializeTimers()
        {
            // Configura e inicializa o timer para a mensagem de status após adicionar uma dívida
            timerStatusAdd = new System.Windows.Forms.Timer { Interval = 2000 };
            timerStatusAdd.Tick += (s, e) =>
            {
                lblStatus.Text = string.Empty; // Limpa o texto da mensagem de status
                timerStatusAdd.Stop(); // Para o timer após executar
            };

            // Configura e inicializa o timer para a mensagem de status após deletar uma dívida
            timerStatusDel = new System.Windows.Forms.Timer { Interval = 2000 };
            timerStatusDel.Tick += (s, e) =>
            {
                lblStatusDel.Text = string.Empty; // Limpa o texto da mensagem de status de deleção
                timerStatusDel.Stop(); // Para o timer após executar
            };

            // Configura e inicializa o timer para a mensagem de status após editar uma dívida
            timerStatusEdit = new System.Windows.Forms.Timer { Interval = 2000 };
            timerStatusEdit.Tick += (s, e) =>
            {
                lblStatusEditar.Text = string.Empty; // Limpa o texto da mensagem de status de edição
                timerStatusEdit.Stop(); // Para o timer após executar
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string desc = txtDesc.Text; // Obtém a descrição da nova dívida do TextBox
            string valor = txtValor.Text; // Obtém o valor da nova dívida do TextBox

            if (string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(valor))
            {
                // Verifica se os campos foram preenchidos
                lblStatus.Text = "Por favor, preencha todos os campos."; // Exibe mensagem de erro
                lblStatus.ForeColor = Color.Red; // Define a cor do texto como vermelho
                timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do método
            }

            // Verifica se a descrição já existe na lista de dívidas
            if (dividas.Any(d => d.Descricao == desc))
            {
                // Se já existe, exibe mensagem de erro
                lblStatus.Text = "Item já existe na lista."; // Exibe mensagem de erro
                lblStatus.ForeColor = Color.Red; // Define a cor do texto como vermelho
                txtDesc.Clear(); // Limpa o campo de descrição
                txtValor.Clear(); // Limpa o campo de valor
                timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do método
            }

            // Adiciona a nova dívida à lista de dívidas
            dividas.Add(new Divida { Descricao = desc, Valor = decimal.Parse(valor) });
            SaveData(); // Salva os dados atualizados

            lblStatus.Text = "Adicionado com sucesso!"; // Exibe mensagem de sucesso
            lblStatus.ForeColor = Color.Green; // Define a cor do texto como verde
            timerStatusAdd.Start(); // Inicia o timer para limpar a mensagem de status
            txtDesc.Clear(); // Limpa o campo de descrição
            txtValor.Clear(); // Limpa o campo de valor
            LoadData(); // Atualiza a exibição na lista de dívidas
        }

        private void LoadData()
        {
            // Carrega os dados do arquivo JSON se o arquivo existir
            if (File.Exists(arquivoDados))
            {
                string json = File.ReadAllText(arquivoDados); // Lê todo o conteúdo do arquivo JSON
                dividas = JsonConvert.DeserializeObject<List<Divida>>(json); // Deserializa o JSON para a lista de dívidas
            }
            else
            {
                dividas = new List<Divida>(); // Se o arquivo não existir, inicializa uma nova lista de dívidas
            }

            // Atualiza a exibição na ListBox e calcula o valor total das dívidas
            lstItems.Items.Clear(); // Limpa os itens da ListBox
            decimal somaTotal = 0; // Variável para somar o valor total das dívidas

            foreach (var divida in dividas)
            {
                somaTotal += divida.Valor; // Soma o valor da dívida ao total
                string valorFormatado = divida.Valor.ToString("C2"); // Formata o valor como moeda brasileira
                lstItems.Items.Add($"---- Descrição: {divida.Descricao} ---- Valor R$: {valorFormatado} ----"); // Adiciona item formatado na ListBox
            }

            string somaTotalFormatada = somaTotal.ToString("C2"); // Formata o total das dívidas como moeda brasileira
            lblSomaValor.Text = $"{somaTotalFormatada}"; // Exibe o total formatado na label
        }

        private void SaveData()
        {
            // Serializa a lista de dívidas para JSON e salva no arquivo especificado
            string json = JsonConvert.SerializeObject(dividas); // Converte a lista de dívidas para JSON
            File.WriteAllText(arquivoDados, json); // Escreve o JSON no arquivo
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string descDel = txtDel.Text; // Obtém a descrição da dívida a ser deletada do TextBox

            if (string.IsNullOrEmpty(descDel))
            {
                // Verifica se o campo de descrição para deletar está vazio
                lblStatusDel.Text = "Preencha o campo deletar."; // Exibe mensagem de erro
                lblStatusDel.ForeColor = Color.Red; // Define a cor do texto como vermelho
                timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do método
            }

            // Busca a dívida pelo nome e remove da lista de dívidas
            var divida = dividas.FirstOrDefault(d => d.Descricao == descDel);

            if (divida != null)
            {
                dividas.Remove(divida); // Remove a dívida da lista de dívidas
                SaveData(); // Salva os dados atualizados
                lblStatusDel.Text = "Deletado com sucesso!"; // Exibe mensagem de sucesso
                timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
                lblStatusDel.ForeColor = Color.Green; // Define a cor do texto como verde
                txtDel.Clear(); // Limpa o campo de descrição para deletar
                LoadData(); // Atualiza a exibição na lista de dívidas
            }
            else
            {
                lblStatusDel.Text = "Item não existe!"; // Exibe mensagem de erro
                lblStatusDel.ForeColor = Color.Red; // Define a cor do texto como vermelho
                txtDel.Clear(); // Limpa o campo de descrição para deletar
                timerStatusDel.Start(); // Inicia o timer para limpar a mensagem de status
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string novaDesc = txtDescEdit.Text; // Obtém a descrição antiga da dívida do TextBox
            string descEdit = txtNovaDescEdit.Text; // Obtém a nova descrição da dívida do TextBox
            string valorEdit = txtNovoValorEdit.Text; // Obtém o novo valor da dívida do TextBox

            if (string.IsNullOrEmpty(descEdit) || string.IsNullOrEmpty(valorEdit))
            {
                // Verifica se os campos de edição estão vazios
                lblStatusEditar.Text = "Preencha todos os campos para editar."; // Exibe mensagem de erro
                lblStatusEditar.ForeColor = Color.Red; // Define a cor do texto como vermelho
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do método
            }

            // Verifica se a nova descrição já existe na lista de dívidas
            if (dividas.Any(d => d.Descricao == descEdit))
            {
                // Se já existe, exibe mensagem de erro
                lblStatusEditar.Text = "Item já existe"; // Exibe mensagem de erro
                lblStatusEditar.ForeColor = Color.Red; // Define a cor do texto como vermelho
                txtDescEdit.Clear(); // Limpa o campo de descrição antiga
                txtNovaDescEdit.Clear(); // Limpa o campo de nova descrição
                txtNovoValorEdit.Clear(); // Limpa o campo de novo valor
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                return; // Sai do método
            }

            // Busca a dívida pelo nome antigo e atualiza na lista de dívidas
            var divida = dividas.FirstOrDefault(d => d.Descricao == novaDesc);

            if (divida != null)
            {
                divida.Descricao = descEdit; // Atualiza a descrição da dívida
                divida.Valor = decimal.Parse(valorEdit); // Atualiza o valor da dívida
                SaveData(); // Salva os dados atualizados
                lblStatusEditar.Text = "Item editado com sucesso!"; // Exibe mensagem de sucesso
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                lblStatusEditar.ForeColor = Color.Green; // Define a cor do texto como verde
                txtDescEdit.Clear(); // Limpa o campo de descrição antiga
                txtNovaDescEdit.Clear(); // Limpa o campo de nova descrição
                txtNovoValorEdit.Clear(); // Limpa o campo de novo valor
                LoadData(); // Atualiza a exibição na lista de dívidas
            }
            else
            {
                lblStatusEditar.Text = "Item não existe!"; // Exibe mensagem de erro
                lblStatusEditar.ForeColor = Color.Red; // Define a cor do texto como vermelho
                timerStatusEdit.Start(); // Inicia o timer para limpar a mensagem de status
                txtDescEdit.Clear(); // Limpa o campo de descrição antiga
                txtNovaDescEdit.Clear(); // Limpa o campo de nova descrição
                txtNovoValorEdit.Clear(); // Limpa o campo de novo valor
            }
        }
    }

    // Classe para representar uma dívida
    public class Divida
    {
        public string? Descricao { get; set; } // Propriedade para a descrição da dívida
        public decimal Valor { get; set; } // Propriedade para o valor da dívida
    }
}
