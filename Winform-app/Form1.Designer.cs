
namespace Winform_app
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvGeneral = new System.Windows.Forms.DataGridView();
            this.pbxPokemon = new System.Windows.Forms.PictureBox();
            this.btnPokemon = new System.Windows.Forms.Button();
            this.btnElemento = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPokemon)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGeneral
            // 
            this.dgvGeneral.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGeneral.Location = new System.Drawing.Point(12, 43);
            this.dgvGeneral.Name = "dgvGeneral";
            this.dgvGeneral.Size = new System.Drawing.Size(556, 200);
            this.dgvGeneral.TabIndex = 0;
            this.dgvGeneral.SelectionChanged += new System.EventHandler(this.dgvPokemon_SelectionChanged);
            // 
            // pbxPokemon
            // 
            this.pbxPokemon.Location = new System.Drawing.Point(616, 43);
            this.pbxPokemon.Name = "pbxPokemon";
            this.pbxPokemon.Size = new System.Drawing.Size(200, 200);
            this.pbxPokemon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxPokemon.TabIndex = 1;
            this.pbxPokemon.TabStop = false;
            // 
            // btnPokemon
            // 
            this.btnPokemon.Location = new System.Drawing.Point(116, 304);
            this.btnPokemon.Name = "btnPokemon";
            this.btnPokemon.Size = new System.Drawing.Size(75, 23);
            this.btnPokemon.TabIndex = 2;
            this.btnPokemon.Text = "Pokemons";
            this.btnPokemon.UseVisualStyleBackColor = true;
            this.btnPokemon.Click += new System.EventHandler(this.btnPokemon_Click);
            // 
            // btnElemento
            // 
            this.btnElemento.Location = new System.Drawing.Point(328, 304);
            this.btnElemento.Name = "btnElemento";
            this.btnElemento.Size = new System.Drawing.Size(75, 23);
            this.btnElemento.TabIndex = 3;
            this.btnElemento.Text = "Elementos";
            this.btnElemento.UseVisualStyleBackColor = true;
            this.btnElemento.Click += new System.EventHandler(this.btnElemento_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 412);
            this.Controls.Add(this.btnElemento);
            this.Controls.Add(this.btnPokemon);
            this.Controls.Add(this.pbxPokemon);
            this.Controls.Add(this.dgvGeneral);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPokemon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGeneral;
        private System.Windows.Forms.PictureBox pbxPokemon;
        private System.Windows.Forms.Button btnPokemon;
        private System.Windows.Forms.Button btnElemento;
    }
}

