using Services;
using Services.DomainModel;
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

namespace TicketPro
{
    public partial class Form1 : Form
    {
        private Boolean panelactivocheck = false;
        private Boolean PanelAsistenciaCheck = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log log = new Log(DateTime.Now, "probando bitacora", TraceLevel.Info);
            LoggerService.WriteLog(log);
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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
