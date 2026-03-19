using System.Drawing;
using System.Windows.Forms;
namespace TicketPro.VistaCliente
{
    partial class DetalleTicket
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTituloFormulario = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlOverlay = new System.Windows.Forms.Panel();
            this.pnlIntegridad = new System.Windows.Forms.Panel();
            this.lblIntegridadTitulo = new System.Windows.Forms.Label();
            this.lblIntegridadDetalle = new System.Windows.Forms.Label();
            this.btnIntegridadVerIgual = new System.Windows.Forms.Button();
            this.btnIntegridadCerrar = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.flowChat = new System.Windows.Forms.FlowLayoutPanel();
            this.panelDetalles = new System.Windows.Forms.Panel();
            this.lblTituloTicket = new System.Windows.Forms.Label();
            this.pnlComentario = new System.Windows.Forms.Panel();
            this.txtNuevoComentario = new System.Windows.Forms.TextBox();
            this.chkComentarioInterno = new System.Windows.Forms.CheckBox();
            this.btnEnviarComentario = new System.Windows.Forms.Button();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnResolver = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlOverlay.SuspendLayout();
            this.pnlIntegridad.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlComentario.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(34)))));
            this.pnlHeader.Controls.Add(this.lblTituloFormulario);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 40);
            this.pnlHeader.TabIndex = 1;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            this.pnlHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseUp);
            // 
            // lblTituloFormulario
            // 
            this.lblTituloFormulario.AutoSize = true;
            this.lblTituloFormulario.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloFormulario.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTituloFormulario.Location = new System.Drawing.Point(12, 11);
            this.lblTituloFormulario.Name = "lblTituloFormulario";
            this.lblTituloFormulario.Size = new System.Drawing.Size(89, 17);
            this.lblTituloFormulario.TabIndex = 0;
            this.lblTituloFormulario.Text = "DetalleTicket";
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnClose.Location = new System.Drawing.Point(860, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // pnlOverlay
            // 
            this.pnlOverlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlOverlay.Controls.Add(this.pnlIntegridad);
            this.pnlOverlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOverlay.Location = new System.Drawing.Point(0, 0);
            this.pnlOverlay.Name = "pnlOverlay";
            this.pnlOverlay.Size = new System.Drawing.Size(1197, 769);
            this.pnlOverlay.TabIndex = 2;
            this.pnlOverlay.Visible = false;
            // 
            // pnlIntegridad
            // 
            this.pnlIntegridad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlIntegridad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlIntegridad.Controls.Add(this.lblIntegridadTitulo);
            this.pnlIntegridad.Controls.Add(this.lblIntegridadDetalle);
            this.pnlIntegridad.Controls.Add(this.btnIntegridadVerIgual);
            this.pnlIntegridad.Controls.Add(this.btnIntegridadCerrar);
            this.pnlIntegridad.Location = new System.Drawing.Point(247, 173);
            this.pnlIntegridad.Name = "pnlIntegridad";
            this.pnlIntegridad.Size = new System.Drawing.Size(700, 300);
            this.pnlIntegridad.TabIndex = 0;
            this.pnlIntegridad.Visible = false;
            // 
            // lblIntegridadTitulo
            // 
            this.lblIntegridadTitulo.AutoSize = true;
            this.lblIntegridadTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblIntegridadTitulo.ForeColor = System.Drawing.Color.White;
            this.lblIntegridadTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblIntegridadTitulo.Name = "lblIntegridadTitulo";
            this.lblIntegridadTitulo.Size = new System.Drawing.Size(288, 30);
            this.lblIntegridadTitulo.TabIndex = 0;
            this.lblIntegridadTitulo.Text = " Integridad comprometida";
            this.lblIntegridadTitulo.Click += new System.EventHandler(this.lblIntegridadTitulo_Click);
            // 
            // lblIntegridadDetalle
            // 
            this.lblIntegridadDetalle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblIntegridadDetalle.ForeColor = System.Drawing.Color.White;
            this.lblIntegridadDetalle.Location = new System.Drawing.Point(20, 70);
            this.lblIntegridadDetalle.MaximumSize = new System.Drawing.Size(660, 0);
            this.lblIntegridadDetalle.Name = "lblIntegridadDetalle";
            this.lblIntegridadDetalle.Size = new System.Drawing.Size(660, 0);
            this.lblIntegridadDetalle.TabIndex = 1;
            // 
            // btnIntegridadVerIgual
            // 
            this.btnIntegridadVerIgual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIntegridadVerIgual.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnIntegridadVerIgual.ForeColor = System.Drawing.Color.White;
            this.btnIntegridadVerIgual.Location = new System.Drawing.Point(380, 240);
            this.btnIntegridadVerIgual.Name = "btnIntegridadVerIgual";
            this.btnIntegridadVerIgual.Size = new System.Drawing.Size(140, 32);
            this.btnIntegridadVerIgual.TabIndex = 2;
            this.btnIntegridadVerIgual.Text = "Ver igualmente";
            this.btnIntegridadVerIgual.Click += new System.EventHandler(this.btnIntegridadVerIgual_Click);
            // 
            // btnIntegridadCerrar
            // 
            this.btnIntegridadCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.btnIntegridadCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIntegridadCerrar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnIntegridadCerrar.ForeColor = System.Drawing.Color.White;
            this.btnIntegridadCerrar.Location = new System.Drawing.Point(540, 240);
            this.btnIntegridadCerrar.Name = "btnIntegridadCerrar";
            this.btnIntegridadCerrar.Size = new System.Drawing.Size(120, 32);
            this.btnIntegridadCerrar.TabIndex = 3;
            this.btnIntegridadCerrar.Text = "Cerrar";
            this.btnIntegridadCerrar.UseVisualStyleBackColor = false;
            this.btnIntegridadCerrar.Click += new System.EventHandler(this.btnIntegridadCerrar_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.pnlBody.Controls.Add(this.flowChat);
            this.pnlBody.Controls.Add(this.panelDetalles);
            this.pnlBody.Controls.Add(this.lblTituloTicket);
            this.pnlBody.Controls.Add(this.pnlComentario);
            this.pnlBody.Controls.Add(this.pnlBotones);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1197, 769);
            this.pnlBody.TabIndex = 0;
            // 
            // flowChat
            // 
            this.flowChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowChat.AutoScroll = true;
            this.flowChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.flowChat.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowChat.Location = new System.Drawing.Point(8, 33);
            this.flowChat.Name = "flowChat";
            this.flowChat.Size = new System.Drawing.Size(760, 642);
            this.flowChat.TabIndex = 0;
            this.flowChat.WrapContents = false;
            // 
            // panelDetalles
            // 
            this.panelDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetalles.AutoScroll = true;
            this.panelDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.panelDetalles.Location = new System.Drawing.Point(774, 3);
            this.panelDetalles.Name = "panelDetalles";
            this.panelDetalles.Padding = new System.Windows.Forms.Padding(10);
            this.panelDetalles.Size = new System.Drawing.Size(417, 720);
            this.panelDetalles.TabIndex = 1;
            // 
            // lblTituloTicket
            // 
            this.lblTituloTicket.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTituloTicket.ForeColor = System.Drawing.Color.White;
            this.lblTituloTicket.Location = new System.Drawing.Point(3, 0);
            this.lblTituloTicket.Name = "lblTituloTicket";
            this.lblTituloTicket.Size = new System.Drawing.Size(649, 30);
            this.lblTituloTicket.TabIndex = 2;
            this.lblTituloTicket.Text = "Ticket_Label";
            // 
            // pnlComentario
            // 
            this.pnlComentario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlComentario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.pnlComentario.Controls.Add(this.txtNuevoComentario);
            this.pnlComentario.Controls.Add(this.chkComentarioInterno);
            this.pnlComentario.Controls.Add(this.btnEnviarComentario);
            this.pnlComentario.Location = new System.Drawing.Point(3, 683);
            this.pnlComentario.Name = "pnlComentario";
            this.pnlComentario.Padding = new System.Windows.Forms.Padding(10);
            this.pnlComentario.Size = new System.Drawing.Size(762, 76);
            this.pnlComentario.TabIndex = 7;
            // 
            // txtNuevoComentario
            // 
            this.txtNuevoComentario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNuevoComentario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.txtNuevoComentario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNuevoComentario.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNuevoComentario.ForeColor = System.Drawing.Color.White;
            this.txtNuevoComentario.Location = new System.Drawing.Point(10, 10);
            this.txtNuevoComentario.Multiline = true;
            this.txtNuevoComentario.Name = "txtNuevoComentario";
            this.txtNuevoComentario.Size = new System.Drawing.Size(745, 26);
            this.txtNuevoComentario.TabIndex = 0;
            // 
            // chkComentarioInterno
            // 
            this.chkComentarioInterno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkComentarioInterno.AutoSize = true;
            this.chkComentarioInterno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkComentarioInterno.ForeColor = System.Drawing.Color.LightGray;
            this.chkComentarioInterno.Location = new System.Drawing.Point(13, 46);
            this.chkComentarioInterno.Name = "chkComentarioInterno";
            this.chkComentarioInterno.Size = new System.Drawing.Size(130, 19);
            this.chkComentarioInterno.TabIndex = 1;
            this.chkComentarioInterno.Text = "Comentario Interno";
            this.chkComentarioInterno.UseVisualStyleBackColor = true;
            // 
            // btnEnviarComentario
            // 
            this.btnEnviarComentario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnviarComentario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.btnEnviarComentario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarComentario.FlatAppearance.BorderSize = 0;
            this.btnEnviarComentario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarComentario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEnviarComentario.ForeColor = System.Drawing.Color.White;
            this.btnEnviarComentario.Location = new System.Drawing.Point(635, 47);
            this.btnEnviarComentario.Name = "btnEnviarComentario";
            this.btnEnviarComentario.Size = new System.Drawing.Size(120, 30);
            this.btnEnviarComentario.TabIndex = 2;
            this.btnEnviarComentario.Text = "Enviar";
            this.btnEnviarComentario.UseVisualStyleBackColor = false;
            this.btnEnviarComentario.Click += new System.EventHandler(this.BtnEnviarComentario_Click);
            // 
            // pnlBotones
            // 
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this.pnlBotones.Controls.Add(this.btnEditar);
            this.pnlBotones.Controls.Add(this.btnResolver);
            this.pnlBotones.Controls.Add(this.btnCerrar);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotones.Location = new System.Drawing.Point(0, 719);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBotones.Size = new System.Drawing.Size(1197, 50);
            this.pnlBotones.TabIndex = 6;
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(771, 10);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(120, 30);
            this.btnEditar.TabIndex = 0;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.BtnEditar_Click);
            // 
            // btnResolver
            // 
            this.btnResolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.btnResolver.FlatAppearance.BorderSize = 0;
            this.btnResolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResolver.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnResolver.ForeColor = System.Drawing.Color.White;
            this.btnResolver.Location = new System.Drawing.Point(911, 10);
            this.btnResolver.Name = "btnResolver";
            this.btnResolver.Size = new System.Drawing.Size(130, 30);
            this.btnResolver.TabIndex = 1;
            this.btnResolver.Text = "Resolver";
            this.btnResolver.UseVisualStyleBackColor = false;
            this.btnResolver.Click += new System.EventHandler(this.BtnResolver_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(1061, 10);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 30);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // DetalleTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1197, 769);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlOverlay);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "DetalleTicket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.DetalleTicketForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlOverlay.ResumeLayout(false);
            this.pnlIntegridad.ResumeLayout(false);
            this.pnlIntegridad.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlComentario.ResumeLayout(false);
            this.pnlComentario.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTituloFormulario;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.FlowLayoutPanel flowChat;
        private System.Windows.Forms.Panel panelDetalles;
        private System.Windows.Forms.Label lblTituloTicket;
        private System.Windows.Forms.Button btnResolver;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Panel pnlOverlay;
        private System.Windows.Forms.Panel pnlIntegridad;
        private System.Windows.Forms.Label lblIntegridadTitulo;
        private System.Windows.Forms.Label lblIntegridadDetalle;
        private System.Windows.Forms.Button btnIntegridadVerIgual;
        private System.Windows.Forms.Button btnIntegridadCerrar;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Panel pnlComentario;
        private System.Windows.Forms.TextBox txtNuevoComentario;
        private System.Windows.Forms.CheckBox chkComentarioInterno;
        private System.Windows.Forms.Button btnEnviarComentario;
    }
}
