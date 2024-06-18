using Newtonsoft.Json;

namespace MinhasDividas
{
    public partial class Form1 : Form
    {
        private List<Divida> dividas;
        private string arquivoDados = "dados.json";
        private System.Windows.Forms.Timer timerStatusAdd;
        private System.Windows.Forms.Timer timerStatusDel;
        private System.Windows.Forms.Timer timerStatusEdit;

        public Form1()
        {
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

            // Verifica se a descrição já existe
            if (dividas.Any(d => d.Descricao == desc))
            {
                lblStatus.Text = "Item já existe na lista.";
                lblStatus.ForeColor = Color.Red;
                txtDesc.Clear();
                txtValor.Clear();
                timerStatusAdd.Start();
                return;
            }

            // Adiciona a nova dívida à lista
            dividas.Add(new Divida { Descricao = desc, Valor = decimal.Parse(valor) });
            SaveData(); // Salva os dados atualizados

            lblStatus.Text = "Adicionado com sucesso!";
            lblStatus.ForeColor = Color.Green;
            timerStatusAdd.Start();
            txtDesc.Clear();
            txtValor.Clear();
            LoadData(); // Atualiza a exibição na lista
        }

        private void LoadData()
        {
            // Carrega os dados do arquivo JSON
            if (File.Exists(arquivoDados))
            {
                string json = File.ReadAllText(arquivoDados);
                dividas = JsonConvert.DeserializeObject<List<Divida>>(json);
            }
            else
            {
                dividas = new List<Divida>();
            }

            // Atualiza a exibição na lista
            lstItems.Items.Clear();
            decimal somaTotal = 0;

            foreach (var divida in dividas)
            {
                somaTotal += divida.Valor;
                string valorFormatado = divida.Valor.ToString("C2");
                lstItems.Items.Add($"---- Descrição: {divida.Descricao} ---- Valor R$: {valorFormatado} ----");
            }

            string somaTotalFormatada = somaTotal.ToString("C2");
            lblSomaValor.Text = $"{somaTotalFormatada}";
        }

        private void SaveData()
        {
            // Salva os dados atuais para o arquivo JSON
            string json = JsonConvert.SerializeObject(dividas);
            File.WriteAllText(arquivoDados, json);
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

            // Busca a dívida pelo nome e remove da lista
            var divida = dividas.FirstOrDefault(d => d.Descricao == descDel);

            if (divida != null)
            {
                dividas.Remove(divida);
                SaveData(); // Salva os dados atualizados
                lblStatusDel.Text = "Deletado com sucesso!";
                timerStatusDel.Start();
                lblStatusDel.ForeColor = Color.Green;
                txtDel.Clear();
                LoadData(); // Atualiza a exibição na lista
            }
            else
            {
                lblStatusDel.Text = "Item não existe!";
                lblStatusDel.ForeColor = Color.Red;
                txtDel.Clear();
                timerStatusDel.Start();
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

            // Verifica se a nova descrição já existe na lista
            if (dividas.Any(d => d.Descricao == descEdit))
            {
                lblStatusEditar.Text = "Item já existe";
                lblStatusEditar.ForeColor = Color.Red;
                txtDescEdit.Clear();
                txtNovaDescEdit.Clear();
                txtNovoValorEdit.Clear();
                timerStatusEdit.Start();
                return;
            }

            // Busca a dívida pelo nome antigo e atualiza na lista
            var divida = dividas.FirstOrDefault(d => d.Descricao == novaDesc);

            if (divida != null)
            {
                divida.Descricao = descEdit;
                divida.Valor = decimal.Parse(valorEdit);
                SaveData(); // Salva os dados atualizados
                lblStatusEditar.Text = "Item editado com sucesso!";
                timerStatusEdit.Start();
                lblStatusEditar.ForeColor = Color.Green;
                txtDescEdit.Clear();
                txtNovaDescEdit.Clear();
                txtNovoValorEdit.Clear();
                LoadData(); // Atualiza a exibição na lista
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

    // Classe para representar uma dívida
    public class Divida
    {
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
