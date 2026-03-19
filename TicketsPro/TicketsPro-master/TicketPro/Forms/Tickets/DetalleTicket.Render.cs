using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Entity.Domain;
using Services.BLL;
namespace TicketPro.VistaCliente
{
    public partial class DetalleTicket
    {
        public void LimpiarChat()
        {
            flowChat.Controls.Clear();
        }
        public void AgregarBurbujaChat(string texto, string autor, DateTime fecha, bool esTecnico)
        {
            var container = new Panel
            {
                Width = flowChat.ClientSize.Width - 30,
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 8),
                BackColor = Color.Transparent
            };
            var burbuja = new Panel
            {
                AutoSize = true,
                MaximumSize = new Size(400, 0),
                BackColor = esTecnico ? Color.FromArgb(70, 130, 180) : Color.FromArgb(80, 80, 90),
                Padding = new Padding(10),
                Margin = new Padding(5),
            };
            var lblAutor = new Label
            {
                Text = $"{autor} - {fecha:dd/MM/yyyy HH:mm}",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.LightGray,
                AutoSize = true
            };
            var lblTexto = new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.White,
                AutoSize = true,
                MaximumSize = new Size(380, 0),
                Location = new Point(0, lblAutor.Bottom + 5)
            };
            burbuja.Controls.Add(lblAutor);
            burbuja.Controls.Add(lblTexto);
            if (!esTecnico)
            {
                burbuja.Left = 0;
            }
            else
            {
                burbuja.Left = container.Width - burbuja.PreferredSize.Width - 10;
            }
            container.Controls.Add(burbuja);
            flowChat.Controls.Add(container);
        }
        public void ScrollChatAlFinal()
        {
            if (flowChat.Controls.Count > 0)
            {
                flowChat.ScrollControlIntoView(flowChat.Controls[flowChat.Controls.Count - 1]);
            }
        }
        public void RenderizarPanelDetalles(
            Ticket ticket, 
            SolucionTicket solucion, 
            string nombreCliente, 
            string nombreTecnico,
            PrioridadTicket prioridad,
            PoliticaSLA politicaSLA,
            List<AdjuntoTicket> adjuntos)
        {
            string tituloDetalles = LanguageBLL.Translate("Titulo_Detalles_Ticket");
            string labelEstado = LanguageBLL.Translate("Estado");
            string labelCategoria = LanguageBLL.Translate("Categoria");
            string labelUbicacion = LanguageBLL.Translate("Ubicacion");
            string labelCliente = LanguageBLL.Translate("Cliente_Label");
            string labelTecnicoAsignado = LanguageBLL.Translate("Tecnico_Asignado");
            string labelEquipoId = LanguageBLL.Translate("Equipo_ID");
            string labelApertura = LanguageBLL.Translate("Apertura");
            string labelResolucion = LanguageBLL.Translate("Resolucion");
            string labelResueltoPor = LanguageBLL.Translate("Resuelto_Por");
            string msgSolucionado = LanguageBLL.Translate("Msg_Solucionado");
            string msgPendiente = LanguageBLL.Translate("Msg_Pendiente");
            string sinResolver = LanguageBLL.Translate("Sin_Resolver");
            panelDetalles.Controls.Clear();
            string fechaApertura = ticket.FechaApertura.ToString("dd/MM/yyyy HH:mm");
            string fechaResolucion = solucion != null
                ? solucion.FechaCierre.ToString("dd/MM/yyyy HH:mm")
                : sinResolver;
            string tecnicoResolvio = solucion != null
                ? nombreTecnico
                : "-";
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(10)
            };
            var lblTituloDetalles = new Label()
            {
                Text = tituloDetalles,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 101, 242),
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 15)
            };
            flowPanel.Controls.Add(lblTituloDetalles);
            string nombreEstado = ticket.Estado?.Nombre ?? "Desconocido";
            var pnlEstado = new Panel
            {
                Width = 350,
                Height = 45,
                BackColor = EstadoColorPanel(nombreEstado),
                Margin = new Padding(0, 0, 0, 10)
            };
            var lblEstado = new Label
            {
                Text = $"{nombreEstado}",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlEstado.Controls.Add(lblEstado);
            flowPanel.Controls.Add(pnlEstado);
            CargarSeccionSLAFlow(ticket, solucion, flowPanel, prioridad, politicaSLA);
            var separador = new Panel
            {
                Width = 350,
                Height = 2,
                BackColor = Color.FromArgb(70, 70, 85),
                Margin = new Padding(0, 10, 0, 10)
            };
            flowPanel.Controls.Add(separador);
            string nombreCategoria = ticket.Categoria?.Nombre ?? LanguageBLL.Translate("No_Disponible");
            string nombreUbicacion = ticket.Ubicacion?.Nombre ?? LanguageBLL.Translate("No_Disponible");
            var labels = new[]
            {
                new Label(){ Text = $"{labelCategoria}:", ForeColor = Color.Gray, AutoSize = true },
                new Label(){ Text = $"  {nombreCategoria}", ForeColor = Color.Gainsboro, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), Margin = new Padding(0, 0, 0, 8) },
                new Label(){ Text = $"{labelUbicacion}:", ForeColor = Color.Gray, AutoSize = true },
                new Label(){ Text = $"  {nombreUbicacion}", ForeColor = Color.Gainsboro, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), Margin = new Padding(0, 0, 0, 8) },
                new Label(){ Text = $"{labelCliente}:", ForeColor = Color.Gray, AutoSize = true },
                new Label(){ Text = $"  {nombreCliente}", ForeColor = Color.Gainsboro, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), Margin = new Padding(0, 0, 0, 8) },
                new Label(){ Text = $"{labelTecnicoAsignado}:", ForeColor = Color.Gray, AutoSize = true },
                new Label(){ Text = $"  {nombreTecnico}", ForeColor = Color.Gainsboro, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), Margin = new Padding(0, 0, 0, 8) },
                new Label(){ Text = $"{labelEquipoId}:", ForeColor = Color.Gray, AutoSize = true },
                new Label(){ Text = $"  {ticket.EquipoAsignado?.Id.ToString() ?? "N/A"}", ForeColor = Color.Gainsboro, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), Margin = new Padding(0, 0, 0, 8) },
                new Label(){ Text = $"{labelApertura}:", ForeColor = Color.Gray, AutoSize = true },
                new Label(){ Text = $"  {fechaApertura}", ForeColor = Color.Gainsboro, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), Margin = new Padding(0, 0, 0, 8) }
            };
            foreach (var lbl in labels)
            {
                lbl.Font = lbl.Font ?? new Font("Segoe UI", 9);
                flowPanel.Controls.Add(lbl);
            }
            var separador2 = new Panel
            {
                Width = 350,
                Height = 2,
                BackColor = Color.FromArgb(70, 70, 85),
                Margin = new Padding(0, 10, 0, 10)
            };
            flowPanel.Controls.Add(separador2);
            var lblResolucionLabel = new Label
            {
                Text = $"{labelResolucion}:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true
            };
            flowPanel.Controls.Add(lblResolucionLabel);
            var lblResolucion = new Label()
            {
                Text = $"  {fechaResolucion}",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = solucion != null ? Color.LightGreen : Color.Gainsboro,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 8)
            };
            flowPanel.Controls.Add(lblResolucion);
            if (solucion != null)
            {
                var lblTecnicoLabel = new Label
                {
                    Text = $"{labelResueltoPor}:",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.Gray,
                    AutoSize = true
                };
                flowPanel.Controls.Add(lblTecnicoLabel);
                var lblTecnicoResolvio = new Label()
                {
                    Text = $"  {tecnicoResolvio}",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.LightGreen,
                    AutoSize = true,
                    Margin = new Padding(0, 0, 0, 15)
                };
                flowPanel.Controls.Add(lblTecnicoResolvio);
                var lblFinal = new Label()
                {
                    Text = $"✓ {msgSolucionado}",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.FromArgb(100, 255, 180),
                    AutoSize = true
                };
                flowPanel.Controls.Add(lblFinal);
            }
            else
            {
                var lblPendiente = new Label()
                {
                    Text = $"⏷ {msgPendiente}",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.FromArgb(255, 180, 100),
                    AutoSize = true
                };
                flowPanel.Controls.Add(lblPendiente);
            }
            CargarSeccionAdjuntosFlow(flowPanel, adjuntos);
            panelDetalles.Controls.Add(flowPanel);
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
        private Color EstadoColorPanel(string nombreEstado)
        {
            if (string.IsNullOrEmpty(nombreEstado))
                return Color.Gray;
            string estadoNormalizado = nombreEstado.Replace(" ", "").ToLower();
            switch (estadoNormalizado)
            {
                case "nuevo":
                    return Color.FromArgb(220, 53, 69);       
                case "asignado":
                    return Color.FromArgb(0, 123, 255);       
                case "enprogreso":
                case "encurso":
                    return Color.FromArgb(255, 193, 7);       
                case "resuelto":
                    return Color.FromArgb(40, 167, 69);       
                case "cerrado":
                    return Color.FromArgb(108, 117, 125);     
                default:
                    return Color.Gray;
            }
        }
        private void CargarSeccionSLAFlow(Ticket ticket, SolucionTicket solucion, FlowLayoutPanel flowPanel, PrioridadTicket prioridad, PoliticaSLA politicaSLA)
        {
            try
            {
                if (prioridad == null)
                    return;
                var pnlPrioridad = new Panel
                {
                    Width = 350,
                    Height = 35,
                    BackColor = ObtenerColorPrioridad(prioridad.Nombre),
                    Margin = new Padding(0, 0, 0, 10)
                };
                var lblPrioridad = new Label
                {
                    Text = $"{LanguageBLL.Translate("Prioridad")}: {prioridad.Nombre}",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlPrioridad.Controls.Add(lblPrioridad);
                flowPanel.Controls.Add(pnlPrioridad);
                if (ticket.PoliticaSLA != null && ticket.FechaVencimiento.HasValue && politicaSLA != null)
                {
                    var lblPoliticaSLA = new Label
                        {
                            Text = $"{LanguageBLL.Translate("Politica_SLA")}: {politicaSLA.Nombre}",
                            Font = new Font("Segoe UI", 9),
                            ForeColor = Color.Gainsboro,
                            AutoSize = true,
                            Margin = new Padding(0, 0, 0, 5)
                        };
                        flowPanel.Controls.Add(lblPoliticaSLA);
                        var lblVencimiento = new Label
                        {
                            Text = $"{LanguageBLL.Translate("Fecha_Vencimiento")}: {ticket.FechaVencimiento.Value:dd/MM/yyyy HH:mm}",
                            Font = new Font("Segoe UI", 9),
                            ForeColor = Color.Gainsboro,
                            AutoSize = true,
                            Margin = new Padding(0, 0, 0, 10)
                        };
                        flowPanel.Controls.Add(lblVencimiento);
                        Color colorSLA;
                        string textoSLA;
                        string iconoSLA;
                        bool ticketResuelto = ticket.Estado?.Nombre == "Resuelto" || ticket.Estado?.Nombre == "Cerrado";
                        if (ticketResuelto && solucion != null)
                        {
                            bool cumplioSLA = solucion.FechaCierre <= ticket.FechaVencimiento.Value;
                            colorSLA = cumplioSLA ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                            textoSLA = cumplioSLA
                                ? LanguageBLL.Translate("SLA_Cumplido")
                                : LanguageBLL.Translate("SLA_Incumplido");
                            iconoSLA = "";
                        }
                        else
                        {
                            TimeSpan tiempoRestante = ticket.FechaVencimiento.Value - DateTime.Now;
                            if (tiempoRestante.TotalHours < 0)
                            {
                                colorSLA = Color.FromArgb(220, 53, 69);
                                double horasVencidas = Math.Abs(tiempoRestante.TotalHours);
                                textoSLA = string.Format(
                                    LanguageBLL.Translate("SLA_Vencido_Hace"),
                                    horasVencidas.ToString("F1")
                                );
                                iconoSLA = "";
                            }
                            else
                            {
                                TimeSpan tiempoTotal = ticket.FechaVencimiento.Value - ticket.FechaApertura;
                                double porcentajeConsumido = ((tiempoTotal - tiempoRestante).TotalHours / tiempoTotal.TotalHours) * 100;
                                if (porcentajeConsumido >= 80)
                                {
                                    colorSLA = Color.FromArgb(255, 193, 7);
                                    textoSLA = string.Format(
                                        LanguageBLL.Translate("SLA_Por_Vencer"),
                                        tiempoRestante.TotalHours.ToString("F1")
                                    );
                                    iconoSLA = "";
                                }
                                else
                                {
                                    colorSLA = Color.FromArgb(40, 167, 69);
                                    textoSLA = string.Format(
                                        LanguageBLL.Translate("SLA_OK"),
                                        tiempoRestante.TotalHours.ToString("F1")
                                    );
                                    iconoSLA = "";
                                }
                            }
                        }
                        var pnlEstadoSLA = new Panel
                        {
                            Width = 350,
                            Height = 38,
                            BackColor = colorSLA,
                            Margin = new Padding(0, 0, 0, 15)
                        };
                        var lblEstadoSLA = new Label
                        {
                            Text = $"{iconoSLA} {textoSLA}",
                            Font = new Font("Segoe UI", 9, FontStyle.Bold),
                            ForeColor = Color.White,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter
                        };
                        pnlEstadoSLA.Controls.Add(lblEstadoSLA);
                        flowPanel.Controls.Add(pnlEstadoSLA);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar SLA: {ex.Message}");
            }
        }
        private void CargarSeccionAdjuntosFlow(FlowLayoutPanel flowPanel, List<AdjuntoTicket> adjuntos)
        {
            try
            {
                var separador = new Panel
                {
                    Width = 350,
                    Height = 2,
                    BackColor = Color.FromArgb(70, 70, 85),
                    Margin = new Padding(0, 10, 0, 10)
                };
                flowPanel.Controls.Add(separador);

                int countAdjuntos = adjuntos?.Count ?? 0;
                var lblTituloAdjuntos = new Label
                {
                    Text = $"{LanguageBLL.Translate("Adjuntos")} ({countAdjuntos})",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.Gainsboro,
                    AutoSize = true,
                    Margin = new Padding(0, 0, 0, 10)
                };
                flowPanel.Controls.Add(lblTituloAdjuntos);

                if (adjuntos != null && adjuntos.Count > 0)
                {
                    foreach (var adjunto in adjuntos)
                    {
                        if (adjunto == null)
                            continue;

                        var pnlAdjunto = new Panel
                        {
                            Width = 350,
                            Height = 35,
                            BackColor = Color.FromArgb(60, 60, 75),
                            Margin = new Padding(0, 0, 0, 5),
                            Cursor = Cursors.Hand,
                            Tag = adjunto
                        };
                        var lblAdjunto = new Label
                        {
                            Text = $"📎 {adjunto.NombreArchivo}",
                            Font = new Font("Segoe UI", 9),
                            ForeColor = Color.Gainsboro,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Padding = new Padding(10, 0, 0, 0)
                        };
                        pnlAdjunto.Controls.Add(lblAdjunto);
                        pnlAdjunto.Click += PnlAdjunto_Click;
                        lblAdjunto.Click += PnlAdjunto_Click;
                        flowPanel.Controls.Add(pnlAdjunto);
                    }
                }

                var btnAdjuntar = new Button
                {
                    Text = LanguageBLL.Translate("Adjuntar_Archivo"),
                    BackColor = Color.FromArgb(76, 132, 255),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    Size = new Size(180, 30),
                    Margin = new Padding(0, 10, 0, 0),
                    Cursor = Cursors.Hand
                };
                btnAdjuntar.FlatAppearance.BorderSize = 0;
                btnAdjuntar.Click += BtnAdjuntar_Click;
                flowPanel.Controls.Add(btnAdjuntar);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar adjuntos: {ex.Message}");
            }
        }
    }
}
