namespace MinhasDividas
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtDesc = new TextBox();
            txtValor = new TextBox();
            btnAdd = new Button();
            lstItems = new ListBox();
            label3 = new Label();
            lblSomaValor = new Label();
            btnDel = new Button();
            btnEditar = new Button();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(125, 32);
            label1.TabIndex = 0;
            label1.Text = "Descrição";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(356, 11);
            label2.Name = "label2";
            label2.Size = new Size(80, 32);
            label2.TabIndex = 1;
            label2.Text = "Valor ";
            // 
            // txtDesc
            // 
            txtDesc.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold);
            txtDesc.Location = new Point(12, 46);
            txtDesc.Name = "txtDesc";
            txtDesc.Size = new Size(338, 25);
            txtDesc.TabIndex = 2;
            txtDesc.TextAlign = HorizontalAlignment.Center;
            // 
            // txtValor
            // 
            txtValor.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold);
            txtValor.Location = new Point(356, 46);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(338, 25);
            txtValor.TabIndex = 3;
            txtValor.TextAlign = HorizontalAlignment.Center;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = SystemColors.Window;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderColor = SystemColors.Window;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Location = new Point(700, 46);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(88, 25);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Adicionar";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // lstItems
            // 
            lstItems.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lstItems.FormattingEnabled = true;
            lstItems.HorizontalScrollbar = true;
            lstItems.ItemHeight = 21;
            lstItems.Location = new Point(12, 96);
            lstItems.Name = "lstItems";
            lstItems.ScrollAlwaysVisible = true;
            lstItems.Size = new Size(776, 277);
            lstItems.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Black", 26.25F, FontStyle.Bold);
            label3.Location = new Point(356, 388);
            label3.Name = "label3";
            label3.Size = new Size(176, 47);
            label3.TabIndex = 6;
            label3.Text = "Total R$:";
            // 
            // lblSomaValor
            // 
            lblSomaValor.AutoSize = true;
            lblSomaValor.Font = new Font("Segoe UI Black", 26.25F, FontStyle.Bold);
            lblSomaValor.ForeColor = Color.Red;
            lblSomaValor.Location = new Point(520, 388);
            lblSomaValor.Name = "lblSomaValor";
            lblSomaValor.Size = new Size(41, 47);
            lblSomaValor.TabIndex = 7;
            lblSomaValor.Text = "0";
            // 
            // btnDel
            // 
            btnDel.BackColor = SystemColors.Window;
            btnDel.Cursor = Cursors.Hand;
            btnDel.FlatAppearance.BorderColor = SystemColors.Window;
            btnDel.FlatStyle = FlatStyle.Flat;
            btnDel.Location = new Point(106, 388);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(88, 29);
            btnDel.TabIndex = 8;
            btnDel.Text = "Deletar";
            btnDel.UseVisualStyleBackColor = false;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = SystemColors.Window;
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.FlatAppearance.BorderColor = SystemColors.Window;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Location = new Point(12, 388);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(88, 29);
            btnEditar.TabIndex = 9;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Location = new Point(12, 426);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 15);
            lblStatus.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(800, 450);
            Controls.Add(lblStatus);
            Controls.Add(btnEditar);
            Controls.Add(btnDel);
            Controls.Add(lblSomaValor);
            Controls.Add(label3);
            Controls.Add(lstItems);
            Controls.Add(btnAdd);
            Controls.Add(txtValor);
            Controls.Add(txtDesc);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Form1";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Minhas Dívida";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtDesc;
        private TextBox txtValor;
        private Button btnAdd;
        private ListBox lstItems;
        private Label label3;
        private Label lblSomaValor;
        private Button btnDel;
        private Button btnEditar;
        private Label lblStatus;
    }
}
