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
            label4 = new Label();
            txtValorEdit = new TextBox();
            txtDescEdit = new TextBox();
            label5 = new Label();
            label6 = new Label();
            txtDel = new TextBox();
            label8 = new Label();
            label9 = new Label();
            lblStatusDel = new Label();
            lblStatusEditar = new Label();
            txtIdEdit = new TextBox();
            label7 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(14, 11);
            label1.Name = "label1";
            label1.Size = new Size(125, 32);
            label1.TabIndex = 0;
            label1.Text = "Descrição";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(407, 11);
            label2.Name = "label2";
            label2.Size = new Size(80, 32);
            label2.TabIndex = 1;
            label2.Text = "Valor ";
            // 
            // txtDesc
            // 
            txtDesc.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold);
            txtDesc.Location = new Point(14, 46);
            txtDesc.Name = "txtDesc";
            txtDesc.Size = new Size(386, 25);
            txtDesc.TabIndex = 2;
            txtDesc.TextAlign = HorizontalAlignment.Center;
            // 
            // txtValor
            // 
            txtValor.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold);
            txtValor.Location = new Point(407, 46);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(386, 25);
            txtValor.TabIndex = 3;
            txtValor.TextAlign = HorizontalAlignment.Center;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = SystemColors.Window;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderColor = SystemColors.Window;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Location = new Point(800, 46);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(101, 25);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Adicionar";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // lstItems
            // 
            lstItems.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lstItems.FormattingEnabled = true;
            lstItems.HorizontalScrollbar = true;
            lstItems.ItemHeight = 45;
            lstItems.Location = new Point(14, 96);
            lstItems.Name = "lstItems";
            lstItems.ScrollAlwaysVisible = true;
            lstItems.Size = new Size(886, 274);
            lstItems.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Black", 26.25F, FontStyle.Bold);
            label3.Location = new Point(407, 375);
            label3.Name = "label3";
            label3.Size = new Size(110, 47);
            label3.TabIndex = 6;
            label3.Text = "Total";
            // 
            // lblSomaValor
            // 
            lblSomaValor.AutoSize = true;
            lblSomaValor.Font = new Font("Segoe UI Black", 26.25F, FontStyle.Bold);
            lblSomaValor.ForeColor = Color.Red;
            lblSomaValor.Location = new Point(506, 375);
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
            btnDel.Location = new Point(800, 546);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(101, 29);
            btnDel.TabIndex = 8;
            btnDel.Text = "Deletar";
            btnDel.UseVisualStyleBackColor = false;
            btnDel.Click += btnDel_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = SystemColors.Window;
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.FlatAppearance.BorderColor = SystemColors.Window;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Location = new Point(299, 596);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(101, 29);
            btnEditar.TabIndex = 9;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Location = new Point(14, 74);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 15);
            lblStatus.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(14, 404);
            label4.Name = "label4";
            label4.Size = new Size(140, 32);
            label4.TabIndex = 11;
            label4.Text = "Editar Item";
            // 
            // txtValorEdit
            // 
            txtValorEdit.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold);
            txtValorEdit.Location = new Point(14, 559);
            txtValorEdit.Name = "txtValorEdit";
            txtValorEdit.Size = new Size(386, 25);
            txtValorEdit.TabIndex = 15;
            txtValorEdit.TextAlign = HorizontalAlignment.Center;
            // 
            // txtDescEdit
            // 
            txtDescEdit.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold);
            txtDescEdit.Location = new Point(14, 509);
            txtDescEdit.Name = "txtDescEdit";
            txtDescEdit.Size = new Size(386, 25);
            txtDescEdit.TabIndex = 14;
            txtDescEdit.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(17, 535);
            label5.Name = "label5";
            label5.Size = new Size(100, 21);
            label5.TabIndex = 13;
            label5.Text = "Novo Valor ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label6.Location = new Point(17, 485);
            label6.Name = "label6";
            label6.Size = new Size(129, 21);
            label6.TabIndex = 12;
            label6.Text = "Nova Descrição";
            // 
            // txtDel
            // 
            txtDel.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold);
            txtDel.Location = new Point(514, 509);
            txtDel.Name = "txtDel";
            txtDel.Size = new Size(386, 25);
            txtDel.TabIndex = 20;
            txtDel.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label8.Location = new Point(518, 485);
            label8.Name = "label8";
            label8.Size = new Size(84, 21);
            label8.TabIndex = 18;
            label8.Text = "Descrição";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(515, 455);
            label9.Name = "label9";
            label9.Size = new Size(156, 32);
            label9.TabIndex = 17;
            label9.Text = "Deletar Item";
            // 
            // lblStatusDel
            // 
            lblStatusDel.AutoSize = true;
            lblStatusDel.ForeColor = Color.Red;
            lblStatusDel.Location = new Point(514, 537);
            lblStatusDel.Name = "lblStatusDel";
            lblStatusDel.Size = new Size(0, 15);
            lblStatusDel.TabIndex = 21;
            // 
            // lblStatusEditar
            // 
            lblStatusEditar.AutoSize = true;
            lblStatusEditar.ForeColor = Color.Red;
            lblStatusEditar.Location = new Point(14, 587);
            lblStatusEditar.Name = "lblStatusEditar";
            lblStatusEditar.Size = new Size(0, 15);
            lblStatusEditar.TabIndex = 22;
            // 
            // txtIdEdit
            // 
            txtIdEdit.Location = new Point(14, 458);
            txtIdEdit.Name = "txtIdEdit";
            txtIdEdit.Size = new Size(389, 24);
            txtIdEdit.TabIndex = 23;
            txtIdEdit.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label7.Location = new Point(17, 434);
            label7.Name = "label7";
            label7.Size = new Size(27, 21);
            label7.TabIndex = 24;
            label7.Text = "ID";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(914, 641);
            Controls.Add(label7);
            Controls.Add(txtIdEdit);
            Controls.Add(lblStatusEditar);
            Controls.Add(lblStatusDel);
            Controls.Add(txtDel);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(txtValorEdit);
            Controls.Add(txtDescEdit);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label4);
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
            Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
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
        private Label label4;
        private TextBox txtValorEdit;
        private TextBox txtDescEdit;
        private Label label5;
        private Label label6;
        private TextBox txtDel;
        private Label label8;
        private Label label9;
        private Label lblStatusDel;
        private Label lblStatusEditar;
        private TextBox txtIdEdit;
        private Label label7;
    }
}
