namespace WindowsFormsPE35
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnInicializa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnInicializa
            // 
            this.BtnInicializa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnInicializa.Location = new System.Drawing.Point(336, 177);
            this.BtnInicializa.Name = "BtnInicializa";
            this.BtnInicializa.Size = new System.Drawing.Size(317, 156);
            this.BtnInicializa.TabIndex = 0;
            this.BtnInicializa.Text = "Inicializa";
            this.BtnInicializa.UseVisualStyleBackColor = true;
            this.BtnInicializa.Click += new System.EventHandler(this.BtnInicializa_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 568);
            this.Controls.Add(this.BtnInicializa);
            this.Name = "Form1";
            this.Text = "Casca Windows Forms para Testes PE35";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnInicializa;
    }
}

