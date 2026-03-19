using Microsoft.Extensions.DependencyInjection;
using BLL;
using BLL.Implementations;
using BLL.Interfaces;
using Entity;
using Entity.Domain;
using Services.BLL;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TicketPro.Inventario;
using TicketPro.Temas;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.Tecnico
{
    public partial class FormGestionInventario : BaseForm, IFormGestionInventarioView
    {
        private List<InventarioItem> _items = new List<InventarioItem>();
        private List<EquipoInformatico> _equipos = new List<EquipoInformatico>();
        private FormGestionInventarioPresenter _presenter;
        public FormGestionInventario()
        {
            InitializeComponent();
            _presenter = new FormGestionInventarioPresenter(
                this,
                Program.ServiceProvider.GetRequiredService<IEquipoBLL>(),
                Program.ServiceProvider.GetRequiredService<IInventarioItemBLL>()
            );
            Load += FormGestionInventario_Load;
        }
        public string FiltroEquipoSeleccionado => cmbFiltroEquipo.SelectedItem?.ToString();
        public string FiltroItemSeleccionado => cmbFiltroItem.SelectedItem?.ToString();
        public string BusquedaEquipo => txtBuscarEquipo.Text;
        public string BusquedaItem => txtBuscarItem.Text;
        public void LlenarFiltros(string[] filtrosEquipos, string[] filtrosItems)
        {
            cmbFiltroEquipo.Items.Clear();
            cmbFiltroEquipo.Items.AddRange(filtrosEquipos);
            cmbFiltroEquipo.SelectedIndex = 1;
            cmbFiltroItem.Items.Clear();
            cmbFiltroItem.Items.AddRange(filtrosItems);
            cmbFiltroItem.SelectedIndex = 2;
        }
        public void RenderizarEquipos(IEnumerable<EquipoInformatico> equiposFiltrados, IEnumerable<InventarioItem> todosLosItems)
        {
            _items = todosLosItems.ToList();
            MostrarEquipos(equiposFiltrados);
        }
        public void RenderizarItems(IEnumerable<InventarioItem> itemsFiltrados, IEnumerable<EquipoInformatico> todosLosEquipos)
        {
            _equipos = todosLosEquipos.ToList();
            MostrarItems(itemsFiltrados);
        }
        public void MostrarMensaje(string mensaje, string titulo = "Informacion")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public bool ConfirmarAccion(string mensaje, string titulo = "Confirmar")
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        public void NotificarExito(string mensaje, string titulo = "Exito")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public string SolicitarRutaExportacionExcel(string defaultFileName)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivos CSV (*.csv)|*.csv";
                sfd.FileName = defaultFileName;
                sfd.Title = LanguageBLL.Translate("Inventario_Gestion_Exportar_TituloDialogo");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    return sfd.FileName;
                }
            }
            return null;
        }
        private void FormGestionInventario_Load(object sender, EventArgs e)
        {
            Theme.Apply(this);
            Theme.MakeResponsiveFlow(flowEquipos);
            Theme.MakeResponsiveFlow(flowItems);
            Theme.ButtonPrimary(btnAgregarEquipo);
            Theme.ButtonPrimary(btnAgregarItem);
            Theme.ButtonNeutral(btnLimpiarEquipo);
            Theme.ButtonNeutral(btnLimpiarItem);
            btnLimpiarEquipo.Tag = "btnLimpiar";
            btnLimpiarItem.Tag = "btnLimpiar";
            txtBuscarEquipo.TextChanged += txtBuscarEquipo_TextChanged;
            txtBuscarItem.TextChanged += txtBuscarItem_TextChanged;
            btnAgregarEquipo.Click += (s, ev) =>
            {
                new FormNuevoEquipo().ShowDialog();
                _presenter.CargarTodo();
            };
            btnAgregarItem.Click += (s, ev) =>
            {
                new FormNuevoItem().ShowDialog();
                _presenter.CargarTodo();
            };
            btnLimpiarEquipo.Click += (s, ev) =>
            {
                txtBuscarEquipo.Clear();
            };
            btnLimpiarItem.Click += (s, ev) =>
            {
                txtBuscarItem.Clear();
            };
            btnExportarEquipos.Click += (s, ev) => _presenter.ExportarEquiposAExcel();
            _presenter.Iniciar();
        }
        private void MostrarEquipos(IEnumerable<EquipoInformatico> equipos)
        {
            string sinProcesador = LanguageBLL.Translate("Sin_Procesador");
            string lblProcesador = LanguageBLL.Translate("Inventario_Gestion_Equipo_Procesador");
            string lblRam = LanguageBLL.Translate("Inventario_Gestion_Equipo_RAM");
            string lblRom = LanguageBLL.Translate("Inventario_Gestion_Equipo_ROM");
            string lblTipo = LanguageBLL.Translate("Inventario_Gestion_Equipo_Tipo");
            string btnComp = LanguageBLL.Translate("Inventario_Gestion_Equipo_BotonVerComponentes");
            string btnDetalle = LanguageBLL.Translate("Inventario_Gestion_Equipo_BotonVerDetalles");
            string btnHistorial = LanguageBLL.Translate("Inventario_Gestion_Equipo_BotonVerHistorial");
            string btnEliminar = LanguageBLL.Translate("Inventario_Gestion_Equipo_BotonEliminar");
            string msgNoComp = LanguageBLL.Translate("Inventario_Gestion_Equipo_MsgNoComponentes");
            string tituloComp = LanguageBLL.Translate("Inventario_Gestion_Equipo_TituloComponentes");
            string tituloConfirm = LanguageBLL.Translate("Confirmar_Eliminacion");
            string msgConfirm = LanguageBLL.Translate("Inventario_Gestion_Equipo_ConfirmarEliminar");
            string tituloExito = LanguageBLL.Translate("Exito");
            string tituloError = LanguageBLL.Translate("Error");
            string msgComponentes = LanguageBLL.Translate("Componentes_De");
            flowEquipos.Controls.Clear();
            foreach (var eq in equipos)
            {
                var card = new Panel
                {
                    Width = 280,
                    Height = 180,
                    Margin = new Padding(10),
                    Padding = new Padding(8),
                    Cursor = Cursors.Hand,
                    Tag = eq
                };
                Theme.MakeCard(card);
                var lblTitulo = new Label
                {
                    Text = $"{eq.ModeloEquipo} (#{eq.NroInventario})",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.LightSkyBlue,
                    AutoSize = true,
                    Location = new Point(10, 10)
                };
                var itemsEq = _items.Where(i => i.EquipoAsignado != null && i.EquipoAsignado.Id == eq.Id).ToList();
                var proc = itemsEq
                    .FirstOrDefault(i => i.CategoriaItem?.Nombre?.Equals("Procesador", StringComparison.OrdinalIgnoreCase) ?? false)
                    ?.Nombre ?? sinProcesador;
                var ram = itemsEq
                    .Where(i => i.CategoriaItem?.Nombre?.Equals("RAM", StringComparison.OrdinalIgnoreCase) ?? false)
                    .Sum(i => i.Valor);
                var rom = itemsEq
                    .Where(i => i.CategoriaItem?.Nombre?.Equals("ROM", StringComparison.OrdinalIgnoreCase) ?? false)
                    .Sum(i => i.Valor);
                var lblDetalles = new Label
                {
                    Text = $"{lblProcesador} {proc}\n{lblRam} {ram} GB | {lblRom} {rom} GB",
                    ForeColor = Theme.TextMuted,
                    AutoSize = true,
                    Location = new Point(10, 35)
                };
                string tipoEquipoNombre = "Desconocido";
                if (eq.TipoEquipo != null)
                {
                    
                    if (string.IsNullOrEmpty(eq.TipoEquipo.Nombre))
                    {
                        var tipoEq = Program.ServiceProvider.GetRequiredService<BLL.Interfaces.ITipoEquipoBLL>()
                            .ObtenerTiposEquipo().FirstOrDefault(t => t.Id == eq.TipoEquipo.Id);
                        tipoEquipoNombre = tipoEq?.Nombre ?? eq.TipoEquipo.Id.ToString();
                    }
                    else
                    {
                        tipoEquipoNombre = eq.TipoEquipo.Nombre;
                    }
                }
                var lblTipoEq = new Label
                {
                    Text = $"{lblTipo} {tipoEquipoNombre}",
                    ForeColor = Color.LightGray,
                    AutoSize = true,
                    Location = new Point(10, 75)
                };
                card.Controls.Add(lblTipoEq);
                string lblUsuario = LanguageBLL.Translate("Inventario_Gestion_Equipo_Usuario");
                string lblUsuarioSin = LanguageBLL.Translate("Inventario_Gestion_Equipo_Usuario_Sin");
                string usuarioAsignado = lblUsuarioSin;
                bool tieneUsuarioAsignado = false;
                if (eq.UsuarioAsignado != null && eq.UsuarioAsignado.IdUsuario != Guid.Empty)
                {
                    tieneUsuarioAsignado = true;
                    
                    if (string.IsNullOrEmpty(eq.UsuarioAsignado.UserName))
                    {
                        var usuario = UsuarioBLL.Instance.GetById(eq.UsuarioAsignado.IdUsuario);
                        if (usuario != null)
                            usuarioAsignado = usuario.UserName;
                    }
                    else
                    {
                        usuarioAsignado = eq.UsuarioAsignado.UserName;
                    }
                }
                var lblUsuarioEq = new Label
                {
                    Text = $"{lblUsuario} {usuarioAsignado}",
                    ForeColor = tieneUsuarioAsignado ? Color.LightGreen : Color.LightGray,
                    AutoSize = true,
                    Location = new Point(10, 95),
                    Font = new Font("Segoe UI", 9, FontStyle.Italic)
                };
                card.Controls.Add(lblUsuarioEq);
                int btnWidth1 = 130;
                int btnWidth2 = 100;
                int btnHeight = 25;
                int yPos1 = 120;
                int yPos2 = 150;
                int xPosLeft = 10;
                int xPosRight = 150;
                var btnVerComp = new Button
                {
                    Text = btnComp,
                    Location = new Point(xPosLeft, yPos1),
                    Size = new Size(btnWidth1, btnHeight)
                };
                Theme.ButtonNeutral(btnVerComp);
                btnVerComp.Tag = "NO_TRANSLATE";
                var btnVerDetalles = new Button
                {
                    Text = btnDetalle,
                    Location = new Point(xPosRight, yPos1),
                    Size = new Size(btnWidth2, btnHeight)
                };
                Theme.ButtonPrimary(btnVerDetalles);
                btnVerDetalles.Tag = "NO_TRANSLATE";
                btnVerDetalles.Click += (s, e) => AbrirDetalleEquipo(eq.Id);
                card.DoubleClick += (s, e) => AbrirDetalleEquipo(eq.Id);
                btnVerComp.Click += (s, e) =>
                {
                    var asociados = _items.Where(i => i.EquipoAsignado != null && i.EquipoAsignado.Id == eq.Id).ToList();
                    MessageBox.Show(
                        asociados.Count == 0
                            ? msgNoComp
                            : string.Join("\n", asociados.Select(i => $"- {i.Nombre} ({i.CategoriaItem?.Nombre ?? (i.CategoriaItem?.Id.ToString() ?? "Sin categoría")})")),
                        $"{msgComponentes} {eq.ModeloEquipo}",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                };
                var btnVerHistorial = new Button
                {
                    Text = btnHistorial,
                    Location = new Point(xPosLeft, yPos2),
                    Size = new Size(btnWidth1, btnHeight)
                };
                Theme.ButtonNeutral(btnVerHistorial);
                btnVerHistorial.Tag = "NO_TRANSLATE";
                btnVerHistorial.Click += (s, e) => AbrirHistorialEquipo(eq.Id);
                var btnEliminarEquipo = new Button
                {
                    Text = btnEliminar,
                    Location = new Point(xPosRight, yPos2),
                    Size = new Size(btnWidth2, btnHeight)
                };
                Theme.ButtonDanger(btnEliminarEquipo);
                btnEliminarEquipo.Tag = "NO_TRANSLATE";
                btnEliminarEquipo.Click += (s, e) =>
                {
                    _presenter.EliminarEquipo(eq);
                };
                card.Controls.Add(lblTitulo);
                card.Controls.Add(lblDetalles);
                card.Controls.Add(btnVerComp);
                card.Controls.Add(btnVerDetalles);
                card.Controls.Add(btnEliminarEquipo);
                card.Controls.Add(btnVerHistorial);
                flowEquipos.Controls.Add(card);
            }
        }
        private void AbrirDetalleEquipo(int equipoId)
        {
            using (var detalle = new FormDetalleEquipo(equipoId))
            {
                detalle.ShowDialog(this);
                _presenter.CargarTodo();
            }
        }
        private void AbrirHistorialEquipo(int equipoId)
        {
            using (var historial = new FormHistorialEquipo(equipoId))
            {
                historial.ShowDialog(this);
            }
        }
        private void MostrarItems(IEnumerable<InventarioItem> items)
        {
            string lblValor = LanguageBLL.Translate("Inventario_Gestion_Item_Valor");
            string lblUbicacion = LanguageBLL.Translate("Inventario_Gestion_Item_Ubicacion");
            string asignadoKey = LanguageBLL.Translate("Inventario_Gestion_Item_Asignacion_Asignado");
            string sinAsignarKey = LanguageBLL.Translate("Inventario_Gestion_Item_Asignacion_SinAsignar");
            string btnEliminar = LanguageBLL.Translate("Inventario_Gestion_Item_BotonEliminar");
            string tituloConfirm = LanguageBLL.Translate("Confirmar_Eliminacion");
            string msgConfirm = LanguageBLL.Translate("Inventario_Gestion_Item_ConfirmarEliminar");
            string tituloExito = LanguageBLL.Translate("Exito");
            string tituloError = LanguageBLL.Translate("Error");
            flowItems.Controls.Clear();
            foreach (var item in items)
            {
                var card = new Panel
                {
                    Width = 260,
                    Height = 140,
                    Margin = new Padding(10),
                    Padding = new Padding(8)
                };
                Theme.MakeCard(card);
                string categoriaNombre = item.CategoriaItem?.Nombre ?? (item.CategoriaItem?.Id.ToString() ?? "Sin categoría");
                var lblTitulo = new Label
                {
                    Text = $"{item.Nombre} ({categoriaNombre})",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(10, 10)
                };
                string ubicacionNombre = item.UbicacionEquipo?.Nombre ?? (item.UbicacionEquipo?.Id.ToString() ?? "N/A");
                var lblDetalles = new Label
                {
                    Text = $"{lblValor} {item.Valor} {item.Unidad}\n{lblUbicacion} {ubicacionNombre}",
                    ForeColor = Theme.TextMuted,
                    Font = new Font("Segoe UI", 9),
                    AutoSize = true,
                    Location = new Point(10, 45)
                };
                bool tieneEquipoAsignado = item.EquipoAsignado != null && item.EquipoAsignado.Id != 0;
                string equipoTexto = tieneEquipoAsignado
                    ? $"{asignadoKey} {_equipos.FirstOrDefault(eq => eq.Id == item.EquipoAsignado.Id)?.ModeloEquipo ?? LanguageBLL.Translate("Desconocido")}"
                    : sinAsignarKey;
                var lblAsignacion = new Label
                {
                    Text = equipoTexto,
                    ForeColor = tieneEquipoAsignado ? Color.LightGreen : Color.LightGray,
                    Font = new Font("Segoe UI", 8, FontStyle.Italic),
                    AutoSize = true,
                    Location = new Point(10, 90)
                };
                var btnEliminarItem = new Button
                {
                    Text = btnEliminar,
                    Location = new Point(150, 95),
                    Size = new Size(90, 28)
                };
                Theme.ButtonDanger(btnEliminarItem);
                btnEliminarItem.Tag = "NO_TRANSLATE";
                btnEliminarItem.Click += (s, e) =>
                {
                    _presenter.EliminarItem(item);
                };
                card.Controls.Add(lblTitulo);
                card.Controls.Add(lblDetalles);
                card.Controls.Add(lblAsignacion);
                card.Controls.Add(btnEliminarItem);
                flowItems.Controls.Add(card);
            }
        }
        private void txtBuscarEquipo_TextChanged(object sender, EventArgs e)
        {
            _presenter.AplicarFiltroEquipos();
        }
        private void txtBuscarItem_TextChanged(object sender, EventArgs e)
        {
            _presenter.AplicarFiltroItems();
        }
    }
}
