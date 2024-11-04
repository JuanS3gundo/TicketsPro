using Abp.Domain.Uow;
using BLL;
using Controller;
using Entity;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Entity.Enums;

namespace TicketPro
{
    public partial class CrearTicket : Form
    {
        private readonly TicketController _ticketController;

        public CrearTicket(TicketController ticketController)
        {
            InitializeComponent();
            _ticketController = ticketController;
            InicializarCombos();
        }

        public CrearTicket()
        {
        }

        private void InicializarCombos()
        {
            // Inicializar el ComboBox de categorías con los valores de la enumeración
            cmbCategoria.DataSource = Enum.GetValues(typeof(Categoria));
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevoTicket = new Ticket
                {
                    IdTicket = Guid.NewGuid(),
                    Titulo = TituloTxt.Text,
                    Descripcion = DescrTxt.Text,
                    FechaApertura = DateTime.Now,
                    Categoria = (Categoria)cmbCategoria.SelectedItem,
                    Estado = Estado.Nuevo, // Estado inicial del ticket
                    TecnicoAsignado = new Tecnico { IdTecnico = 1 } // Asigna una instancia de Tecnico con IdTecnico = 1

                };

                _ticketController.CrearTicket(nuevoTicket);
                MessageBox.Show("Ticket creado correctamente.");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el ticket: {ex.Message}");
            }
        }
    }
}
