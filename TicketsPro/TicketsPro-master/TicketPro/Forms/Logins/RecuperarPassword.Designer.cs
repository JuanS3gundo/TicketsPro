using System.Drawing;
using System.Windows.Forms;
namespace TicketPro.Logins
{
    partial class RecuperarPassword
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.pnlUserInput = new System.Windows.Forms.Panel();
            this.lblUserIcon = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnEnviarCodigo = new System.Windows.Forms.Button();
            this.btnVolverLogin = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlUserInput.SuspendLayout();
            this.SuspendLayout();
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(40);
            this.pnlMain.Size = new System.Drawing.Size(600, 500);
            this.pnlMain.TabIndex = 0;
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.lblTitulo);
            this.pnlContent.Controls.Add(this.lblSubtitulo);
            this.pnlContent.Controls.Add(this.lblUser);
            this.pnlContent.Controls.Add(this.pnlUserInput);
            this.pnlContent.Controls.Add(this.lblEstado);
            this.pnlContent.Controls.Add(this.btnEnviarCodigo);
            this.pnlContent.Controls.Add(this.btnVolverLogin);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(40, 40);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(50, 40, 50, 40);
            this.pnlContent.Size = new System.Drawing.Size(520, 420);
            this.pnlContent.TabIndex = 0;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.lblTitulo.Location = new System.Drawing.Point(50, 40);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(355, 45);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Recuperar Contrasena";
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(50, 95);
            this.lblSubtitulo.MaximumSize = new System.Drawing.Size(400, 0);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(385, 38);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Ingresa tu nombre de usuario y te enviaremos un codigo de verificacion a tu corre" +
    "o electronico.";
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblUser.Location = new System.Drawing.Point(50, 155);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(141, 19);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Nombre de Usuario";
            this.pnlUserInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlUserInput.Controls.Add(this.lblUserIcon);
            this.pnlUserInput.Controls.Add(this.txtUser);
            this.pnlUserInput.Location = new System.Drawing.Point(50, 180);
            this.pnlUserInput.Name = "pnlUserInput";
            this.pnlUserInput.Size = new System.Drawing.Size(400, 45);
            this.pnlUserInput.TabIndex = 3;
            this.lblUserIcon.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblUserIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblUserIcon.Location = new System.Drawing.Point(10, 8);
            this.lblUserIcon.Name = "lblUserIcon";
            this.lblUserIcon.Size = new System.Drawing.Size(30, 30);
            this.lblUserIcon.TabIndex = 0;
            this.lblUserIcon.Text = "";
            this.lblUserIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtUser.Location = new System.Drawing.Point(50, 12);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(335, 22);
            this.txtUser.TabIndex = 1;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEstado.Location = new System.Drawing.Point(50, 235);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(400, 30);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = " ";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviarCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnEnviarCodigo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarCodigo.FlatAppearance.BorderSize = 0;
            this.btnEnviarCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarCodigo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnEnviarCodigo.ForeColor = System.Drawing.Color.White;
            this.btnEnviarCodigo.Location = new System.Drawing.Point(50, 275);
            this.btnEnviarCodigo.Name = "btnEnviarCodigo";
            this.btnEnviarCodigo.Size = new System.Drawing.Size(400, 45);
            this.btnEnviarCodigo.TabIndex = 5;
            this.btnEnviarCodigo.Text = "Enviar Codigo";
            this.btnEnviarCodigo.UseVisualStyleBackColor = false;
            this.btnEnviarCodigo.Click += new System.EventHandler(this.btnEnviarCodigo_Click_1);
            this.btnVolverLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnVolverLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolverLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnVolverLogin.FlatAppearance.BorderSize = 2;
            this.btnVolverLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolverLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnVolverLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnVolverLogin.Location = new System.Drawing.Point(50, 335);
            this.btnVolverLogin.Name = "btnVolverLogin";
            this.btnVolverLogin.Size = new System.Drawing.Size(400, 40);
            this.btnVolverLogin.TabIndex = 6;
            this.btnVolverLogin.Text = "a† Volver al Login";
            this.btnVolverLogin.UseVisualStyleBackColor = false;
            this.btnVolverLogin.Click += new System.EventHandler(this.btnVolverLogin_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(550, 450);
            this.Name = "RecuperarPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recuperar Contrasena - TicketsPro";
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlUserInput.ResumeLayout(false);
            this.pnlUserInput.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion
        private Panel pnlMain;
        private Panel pnlContent;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblUser;
        private Panel pnlUserInput;
        private Label lblUserIcon;
        private TextBox txtUser;
        private Label lblEstado;
        private Button btnEnviarCodigo;
        private Button btnVolverLogin;
    }
}
