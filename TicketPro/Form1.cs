using Controller;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.DomainModel;
using Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Entity.Enums;

namespace TicketPro
{
    public partial class Form1 : Form
    {
        private Boolean panelactivocheck = false;
        private Boolean PanelAsistenciaCheck = false;
        private readonly TicketController _ticketController;
        private readonly IServiceProvider _serviceProvider;



        public Form1(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            CargarTickets();    

        }

        private readonly ILoggerService _loggerService;
        private void button1_Click(object sender, EventArgs e)
        {
            Log log = new Log(DateTime.Now, "probando bitacora", TraceLevel.Info);
            _loggerService.WriteLog(log);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            panelactivocheck = !panelactivocheck;
            Panelactivos();
        }
        private void Panelactivos()
        {
            if (panelactivocheck)
            {
                panelActivos.Height = 112;
            }
            else
            {
                panelActivos.Height = 0;
            }
            if (PanelAsistenciaCheck)
            {
                PanelAsistencia.Height = 112;
            }
            else
            {
                PanelAsistencia.Height = 0;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            PanelAsistenciaCheck = !PanelAsistenciaCheck;
            Panelactivos();
        }

        private void CargarTickets()
        {
            try
            {
                // Llama al controlador para obtener la lista de tickets
                var tickets = _ticketController.ObtenerTodosLosTickets();

                // Asigna la lista de tickets como el origen de datos del DataGridView
                dataGridView1.DataSource = tickets;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los tickets: {ex.Message}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            var crearTicketForm = _serviceProvider.GetRequiredService<CrearTicket>();
            crearTicketForm.ShowDialog();
        }
    }
}
