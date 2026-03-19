using Microsoft.VisualBasic;
using Services.BLL;
using Services.DomainModel;
using Services.DomainModel.Composite;
using Services.DTOs;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicketPro.MVP.Presenters;
using TicketPro.MVP.Views;
namespace TicketPro
{
    public partial class FormGestionAccesos : BaseForm, IFormGestionAccesosView
    {
        private FormGestionAccesosPresenter _presenter;
        public FormGestionAccesos()
        {
            InitializeComponent();
            _presenter = new FormGestionAccesosPresenter(this);
            this.Load += FormGestionAccesos_Load;
            cmbUsuarios.SelectedIndexChanged += cmbUsuarios_SelectedIndexChanged;
            cmbFamilias.SelectedIndexChanged += cmbFamilias_SelectedIndexChanged;
            SetListProps(lstFamDisp);
            SetListProps(lstFamAsig);
            SetListProps(lstPatDisp);
            SetListProps(lstPatAsig);
            SetListProps(lstTodasFamilias, SelectionMode.One);
            SetListProps(lstTodasPatentes, SelectionMode.One);
            btnFamAdd.Click += (s, e) => { MoverSeleccion(lstFamDisp, lstFamAsig); };
            btnFamRem.Click += (s, e) => { MoverSeleccion(lstFamAsig, lstFamDisp); };
            btnPatAdd.Click += (s, e) => { MoverSeleccion(lstPatDisp, lstPatAsig); };
            btnPatRem.Click += (s, e) => { MoverSeleccion(lstPatAsig, lstPatDisp); };
            btnGuardar.Click += btnGuardar_Click;
            btnNuevaFamilia.Click += btnNuevaFamilia_Click;
            btnEliminarFamilia.Click += btnEliminarFamilia_Click;
            btnNuevaPatente.Click += btnNuevaPatente_Click;
            btnEliminarPatente.Click += btnEliminarPatente_Click;
            btnGuardarPatentesParaFamilia.Click += btnGuardarPatentesParaFamilia_Click;
            btnRenombrarFamilia.Click += btnRenombrarFamilia_Click;
            button1.Click += button1_Click; 
            this.tabControlAccesos.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlAccesos_DrawItem);
        }
        private void SetListProps(ListBox lb, SelectionMode mode = SelectionMode.MultiExtended)
        {
            lb.DisplayMember = "Nombre";
            lb.ValueMember = "Id";
            lb.SelectionMode = mode;
        }
        private void FormGestionAccesos_Load(object sender, EventArgs e)
        {
            _presenter.Iniciar();
        }
        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedItem is Usuario u)
                _presenter.CargarListasUsuario(u.IdUsuario);
            else
                LimpiarListasUsuario();
        }
        private void cmbFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFamilias.SelectedItem is Familia familia)
                _presenter.CargarPatentesDeFamilia(familia.IdFamilia);
        }
        private void btnGuardar_Click(object sender, EventArgs e) => _presenter.GuardarAccesosUsuario();
        private void btnNuevaFamilia_Click(object sender, EventArgs e) => _presenter.NuevaFamilia();
        private void btnEliminarFamilia_Click(object sender, EventArgs e) => _presenter.EliminarFamilia();
        private void btnNuevaPatente_Click(object sender, EventArgs e) => _presenter.NuevaPatente();
        private void btnEliminarPatente_Click(object sender, EventArgs e) => _presenter.EliminarPatente();
        private void btnGuardarPatentesParaFamilia_Click(object sender, EventArgs e) => _presenter.GuardarPatentesParaFamilia();
        private void btnRenombrarFamilia_Click(object sender, EventArgs e) => _presenter.RenombrarFamilia();
        private void button1_Click(object sender, EventArgs e) => _presenter.EliminarUsuario();
        private void MoverSeleccion(ListBox desde, ListBox hacia)
        {
            if (desde.SelectedItems.Count == 0) return;
            var src = (desde.DataSource as List<DisplayItemDTO>) ?? new List<DisplayItemDTO>();
            var dst = (hacia.DataSource as List<DisplayItemDTO>) ?? new List<DisplayItemDTO>();
            var mover = desde.SelectedItems.Cast<DisplayItemDTO>().ToList();
            foreach (var it in mover)
            {
                src.Remove(it);
                dst.Add(it);
            }
            desde.DataSource = src.OrderBy(i => i.Display).ToList();
            hacia.DataSource = dst.OrderBy(i => i.Display).ToList();
        }
        public Usuario UsuarioSeleccionado => cmbUsuarios.SelectedItem as Usuario;
        public Familia FamiliaSeleccionada => cmbFamilias.SelectedItem as Familia;
        public Familia FamiliaGlobalSeleccionada => lstTodasFamilias.SelectedItem as Familia;
        public Patente PatenteGlobalSeleccionada => lstTodasPatentes.SelectedItem as Patente;
        public string NuevoNombreFamiliaIngresado => txtNuevoNombreFamilia.Text;
        public void LlenarComboUsuarios(IEnumerable<Usuario> usuarios)
        {
            cmbUsuarios.DataSource = usuarios.ToList();
            cmbUsuarios.DisplayMember = "UserName";
            cmbUsuarios.ValueMember = "IdUsuario";
        }
        public void LlenarComboFamilias(IEnumerable<Familia> familias, Guid? idSeleccionado = null)
        {
            cmbFamilias.DataSource = familias.ToList();
            cmbFamilias.DisplayMember = "NombreFamilia";
            cmbFamilias.ValueMember = "IdFamilia";
            if (idSeleccionado.HasValue)
                cmbFamilias.SelectedValue = idSeleccionado.Value;
        }
        public void LlenarListaTodasFamilias(IEnumerable<Familia> familias)
        {
            lstTodasFamilias.DataSource = familias.ToList();
            lstTodasFamilias.DisplayMember = "NombreFamilia";
            lstTodasFamilias.ValueMember = "IdFamilia";
        }
        public void LlenarListaTodasPatentes(IEnumerable<Patente> patentes)
        {
            lstTodasPatentes.DataSource = patentes.ToList();
            lstTodasPatentes.DisplayMember = "Nombre";
            lstTodasPatentes.ValueMember = "idPatente";
        }
        public void MostrarListasUsuario(IEnumerable<DisplayItemDTO> famAsig, IEnumerable<DisplayItemDTO> famDisp, IEnumerable<DisplayItemDTO> patAsig, IEnumerable<DisplayItemDTO> patDisp)
        {
            lstFamAsig.DataSource = famAsig.ToList();
            lstFamDisp.DataSource = famDisp.ToList();
            lstPatAsig.DataSource = patAsig.ToList();
            lstPatDisp.DataSource = patDisp.ToList();
        }
        public void LimpiarListasUsuario()
        {
            lstFamDisp.DataSource = lstFamAsig.DataSource = null;
            lstPatDisp.DataSource = lstPatAsig.DataSource = null;
        }
        public void MostrarPatentesDeFamilia(IEnumerable<DisplayItemDTO> patAsig, IEnumerable<DisplayItemDTO> patDisp)
        {
        }
        public void LlenarCheckPatentesParaFamilia(IEnumerable<DisplayItemDTO> todasPatentes, IEnumerable<Guid> idsAsignadas)
        {
            chkPatentesParaFamilia.Items.Clear();
            foreach (var p in todasPatentes)
            {
                chkPatentesParaFamilia.Items.Add(p, idsAsignadas.Contains(p.Id));
            }
        }
        public List<Guid> ObtenerFamiliasAsignadasUsuarioIds() => 
            (lstFamAsig.DataSource as List<DisplayItemDTO>)?.Select(i => i.Id).ToList() ?? new List<Guid>();
        public List<Guid> ObtenerPatentesAsignadasUsuarioIds() => 
            (lstPatAsig.DataSource as List<DisplayItemDTO>)?.Select(i => i.Id).ToList() ?? new List<Guid>();
        public List<Guid> ObtenerPatentesDisponiblesFamiliaSeleccionadasIds() => new List<Guid>();
        public List<Guid> ObtenerPatentesAsignadasFamiliaSeleccionadasIds() => new List<Guid>();
        public List<Guid> ObtenerPatentesCheckeadasFamiliaIds()
        {
            return chkPatentesParaFamilia.CheckedItems.Cast<DisplayItemDTO>().Select(i => i.Id).ToList();
        }
        public void MostrarMensajeExito(string mensaje, string titulo) => MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        public void MostrarMensajeAdvertencia(string mensaje, string titulo) => MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        public new void MostrarError(string mensaje, Exception ex = null)
        {
            base.MostrarError(mensaje, ex);
        }
        public bool ConfirmarAccion(string mensaje, string titulo)
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        public string PedirValor(string mensaje, string titulo)
        {
            return Interaction.InputBox(mensaje, titulo);
        }
        public void SetearTituloFormulario(string titulo)
        {
            this.Text = titulo;
        }
        public void RefrescarPermisosUI()
        {
            SessionService.RefreshPermisos(); 
            var main = Application.OpenForms.OfType<VistaMainForm>().FirstOrDefault();
            if (main != null) main.AplicarPermisos();
        }
        private void tabControlAccesos_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var g = e.Graphics;
                var tabPage = this.tabControlAccesos.TabPages[e.Index];
                var tabRect = this.tabControlAccesos.GetTabRect(e.Index);
                var tabColor = Color.FromArgb(45, 45, 60);
                var selectedColor = Color.FromArgb(55, 55, 75);
                var textColor = Color.White;
                var textFont = new Font("Segoe UI", 9F, FontStyle.Bold);
                if (e.State == DrawItemState.Selected)
                {
                    using (var bgBrush = new SolidBrush(selectedColor))
                    {
                        g.FillRectangle(bgBrush, e.Bounds);
                    }
                }
                else
                {
                    using (var bgBrush = new SolidBrush(tabColor))
                    {
                        g.FillRectangle(bgBrush, e.Bounds);
                    }
                }
                TextRenderer.DrawText(g, tabPage.Text, textFont, tabRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                textFont.Dispose();
            }
            catch (Exception ex)
            {
                MostrarError("Error al dibujar pestanas", ex);
            }
        }
        private void btnGuardar_Click_1(object sender, EventArgs e) { }
        private void btnPatAdd_Click(object sender, EventArgs e) { }
    }
}
