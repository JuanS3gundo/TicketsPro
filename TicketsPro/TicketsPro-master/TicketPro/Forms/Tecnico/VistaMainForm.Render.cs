using System;
using System.Drawing;
using System.Windows.Forms;
using Entity.Domain;
using Services.BLL;
using Microsoft.Extensions.DependencyInjection;
namespace TicketPro
{
    public partial class VistaMainForm
    {
        private void AjustarTarjetas()
        {
            try
            {
                foreach (Panel card in flowTickets.Controls)
                {
                    int newWidth = flowTickets.ClientSize.Width - 40;
                    card.Width = newWidth;
                    foreach (Control c in card.Controls)
                        if (c is Label lbl)
                            lbl.Width = newWidth - 30;
                }
            }
            catch { }
        }
        private Panel CrearTarjetaTicket(Ticket t, string tecnicoNombre)
        {
            var solucion = Program.ServiceProvider.GetRequiredService<BLL.Interfaces.ISolucionTicketBLL>().ObtenerSolucionPorTicket(t.Id);
            string sinResolver = LanguageBLL.Translate("Sin_Resolver");
            string idLabel = LanguageBLL.Translate("ID");
            string estadoLabel = LanguageBLL.Translate("Estado");
            string resueltoLabel = LanguageBLL.Translate("Resuelto_el");
            string categoriaLabel = LanguageBLL.Translate("Categoria");
            string ubicacionLabel = LanguageBLL.Translate("Ubicacion");
            string tecnicoLabel = LanguageBLL.Translate("Tecnico");
            string equipoIdLabel = LanguageBLL.Translate("Equipo_ID");
            string aperturaLabel = LanguageBLL.Translate("Apertura");
            string resolucionLabel = LanguageBLL.Translate("Resolucion");
            string noAsignado = LanguageBLL.Translate("No_asignado");
            string fechaResolucion = solucion != null
                ? solucion.FechaCierre.ToString("dd/MM/yyyy HH:mm")
                : sinResolver;
            string fechaApertura = t.FechaApertura.ToString("dd/MM/yyyy HH:mm");
            var card = new Panel
            {
                Width = flowTickets.ClientSize.Width - 40,
                Height = 200,
                BackColor = Color.FromArgb(45, 45, 60),
                Margin = new Padding(10),
                Padding = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = t.Id
            };
            var estadoPanel = new Panel
            {
                BackColor = EstadoColor(t.Estado?.Nombre ?? string.Empty),
                Dock = DockStyle.Left,
                Width = 6
            };
            card.Controls.Add(estadoPanel);
            string idCorto = t.Id.ToString().Substring(0, 4).ToUpper();
            var lblId = new Label
            {
                Text = solucion != null
                    ? $"{idLabel}: {idCorto}   |   {estadoLabel}: {t.Estado?.Nombre ?? t.Estado?.Id.ToString()}   |   {resueltoLabel} {fechaResolucion}"
                    : $"{idLabel}: {idCorto}   |   {estadoLabel}: {t.Estado?.Nombre ?? t.Estado?.Id.ToString()}",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = false,
                Location = new Point(15, 5),
                Width = card.Width - 30,
                Height = 15
            };
            var lblTituloTicket = new Label
            {
                Text = t.Titulo,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Location = new Point(15, 22),
                Width = card.Width - 30,
                Height = 22
            };
            string desc = t.Descripcion ?? "";
            if (desc.Length > 120)
                desc = desc.Substring(0, 117) + "...";
            var lblDesc = new Label
            {
                Text = desc,
                ForeColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 9),
                AutoSize = false,
                Location = new Point(15, 45),
                Width = card.Width - 30,
                Height = 35
            };
            var lblDetalles = new Label
            {
                Text =
                    $"{categoriaLabel}: {t.Categoria?.Nombre ?? t.Categoria?.Id.ToString()}\n" +
                    $"{ubicacionLabel}: {t.Ubicacion?.Nombre ?? t.Ubicacion?.Id.ToString()}\n" +
                    $"{tecnicoLabel}: {tecnicoNombre}   -   {equipoIdLabel}: {t.EquipoAsignado?.Id.ToString() ?? noAsignado}\n" +
                    $"{aperturaLabel}: {fechaApertura}\n" +
                    $"{resolucionLabel}: {fechaResolucion}",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 8),
                AutoSize = false,
                Location = new Point(15, 80),
                Width = card.Width - 30,
                Height = 110
            };
            EventHandler open = (s, e) =>
            {
                try
                {
                    var idTicket = (Guid)card.Tag;
                    var form = new TicketPro.VistaCliente.DetalleTicket(idTicket)
                    {
                        StartPosition = FormStartPosition.CenterParent
                    };
                    form.ShowDialog();
                    _presenter.CargarTickets();
                }
                catch (Exception ex)
                {
                    MostrarError("Error al abrir el ticket", ex);
                }
            };
            card.Click += open;
            lblId.Click += open;
            lblTituloTicket.Click += open;
            lblDesc.Click += open;
            lblDetalles.Click += open;
            card.Controls.Add(lblId);
            card.Controls.Add(lblTituloTicket);
            card.Controls.Add(lblDesc);
            card.Controls.Add(lblDetalles);
            if (t.Prioridad != null && t.Prioridad.Id != Guid.Empty)
            {
                try
                {
                    var prioridad = t.Prioridad;
                    
                    if (string.IsNullOrEmpty(prioridad.Nombre))
                    {
                        prioridad = Program.ServiceProvider.GetRequiredService<BLL.Interfaces.IPrioridadBLL>().ObtenerPorId(t.Prioridad.Id);
                    }
                    if (prioridad != null)
                    {
                        var pnlPrioridad = new Panel
                        {
                            Width = 85,
                            Height = 28,
                            Location = new Point(card.Width - 95, 8),
                            BackColor = ObtenerColorPrioridad(prioridad.Nombre)
                        };
                        var lblPrioridad = new Label
                        {
                            Text = $"{prioridad.Nombre}",
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            ForeColor = Color.White,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Dock = DockStyle.Fill
                        };
                        pnlPrioridad.Controls.Add(lblPrioridad);
                        card.Controls.Add(pnlPrioridad);
                        pnlPrioridad.BringToFront();
                    }
                }
                catch (Exception)
                {
                }
            }
            if (t.FechaVencimiento.HasValue && t.Estado?.Nombre != "Resuelto" && t.Estado?.Nombre != "Cerrado")
            {
                try
                {
                    TimeSpan tiempoRestante = t.FechaVencimiento.Value - DateTime.Now;
                    Color colorSLA;
                    string textoSLA;
                    if (tiempoRestante.TotalHours < 0)
                    {
                        colorSLA = Color.FromArgb(220, 53, 69); 
                        textoSLA = $"{LanguageBLL.Translate("Vencido")}";
                    }
                    else
                    {
                        TimeSpan tiempoTotal = t.FechaVencimiento.Value - t.FechaApertura;
                        double porcentaje = ((tiempoTotal - tiempoRestante).TotalHours / tiempoTotal.TotalHours) * 100;
                        if (porcentaje >= 80)
                        {
                            colorSLA = Color.FromArgb(255, 193, 7); 
                            textoSLA = $"{tiempoRestante.TotalHours:F0}h";
                        }
                        else
                        {
                            colorSLA = Color.FromArgb(40, 167, 69); 
                            textoSLA = $"{tiempoRestante.TotalHours:F0}h";
                        }
                    }
                    var pnlSLA = new Panel
                    {
                        Width = 100,
                        Height = 25,
                        Location = new Point(card.Width - 110, card.Height - 35),
                        BackColor = colorSLA
                    };
                    var lblSLA = new Label
                    {
                        Text = textoSLA,
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    pnlSLA.Controls.Add(lblSLA);
                    card.Controls.Add(pnlSLA);
                    pnlSLA.BringToFront();
                }
                catch (Exception)
                {
                }
            }
            return card;
        }
        private Color EstadoColor(string nombreEstado)
        {
            if (string.IsNullOrEmpty(nombreEstado))
            {
                return Color.Gray;
            }
            string estadoNormalizado = nombreEstado.Trim().Replace(" ", "").ToLower();
            Color color;
            switch (estadoNormalizado)
            {
                case "nuevo":
                    color = Color.FromArgb(220, 53, 69);       
                    break;
                case "asignado":
                    color = Color.FromArgb(0, 123, 255);       
                    break;
                case "enprogreso":
                case "encurso":
                case "proceso":
                    color = Color.FromArgb(255, 193, 7);       
                    break;
                case "resuelto":
                case "solucionado":
                    color = Color.FromArgb(40, 167, 69);       
                    break;
                case "cerrado":
                case "finalizado":
                    color = Color.FromArgb(108, 117, 125);     
                    break;
                default:
                    color = Color.Gray;                         
                    break;
            }
            return color;
        }
        private Color ObtenerColorPrioridad(string nombrePrioridad)
        {
            switch (nombrePrioridad?.ToLower())
            {
                case "critica":
                    return Color.FromArgb(139, 0, 0); 
                case "alta":
                    return Color.FromArgb(220, 53, 69); 
                case "media":
                    return Color.FromArgb(255, 193, 7); 
                case "baja":
                    return Color.FromArgb(40, 167, 69); 
                default:
                    return Color.Gray;
            }
        }
    }
}
