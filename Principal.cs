using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; 
using Pantallas_alto_volumen_de_datos.User;
using Pantallas_alto_volumen_de_datos.DAO;
using CassandraEnlaceServer;

namespace Pantallas_alto_volumen_de_datos
{
    public partial class Principal : Form
    {
        public EmpleadosCassandra usuarioConectado = new EmpleadosCassandra();
        public DateTime OperationDate;
        EnlaceCassandra enlace = new EnlaceCassandra();

        public Principal(EmpleadosCassandra usuario, DateTime fecha)
        {
            InitializeComponent();
            this.usuarioConectado = usuario;
            this.OperationDate = fecha;
            usuarioConectadoTXT.Text = usuario.email;
            OperationDateText.Text = fecha.ToString("dd/MM/yyyy");
            Size size = TextRenderer.MeasureText(OperationDateText.Text, OperationDateText.Font);
            OperationDateText.Width = size.Width;
            OperationDateText.Height = size.Height;
            AbrirFormHija(new inicio());

            // Control de accesos según tipo de usuario
            if (!usuarioConectado.admin)
            {
                subMenuReportes.Visible = false;
                btnReportes.Visible = false;
                btnRepOcu.Visible = false;
                btnRepVentas.Visible = false;
                btnCliHist.Visible = false;

                btnEmp.Visible = false;
                btnFacturas.Visible = true;

                panel2.Visible = false;
                panel16.Visible = false;
                panel8.Visible = false;

                //btnCancelaciones.Enabled = false;
               // btnCancelaciones.Visible = false;
                btnHoteles.Visible = false;

                btnReservacion.Location = new Point(btnHoteles.Location.X, btnHoteles.Location.Y); // Ajustar la posición del botón de Reservaciones
                subMenuReser.Location = new Point(submenuH.Location.X, submenuH.Location.Y); // Ajustar la posición del submenú de Reservaciones

            }
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro de que desea cerrar sesion?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                Login window = new Login();
                window.ShowDialog();
                this.Close();
            }
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Minimized;
        }

        

        //private void Principal_Load(object sender, EventArgs e) //intente hacer que cuando corra el programa se ponga la ventana inicio en el panel pero no pude xd
        //{
        //    pictureBox1_Click(null , e);

        //}
        //este codigo es para poder mover la ventana 
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")] 
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL",EntryPoint ="SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

     

        private void btnRepOcu_Click(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            AbrirFormHija(new RepOcupacion());
        }




        private void btnRepVentas_Click(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            AbrirFormHija(new RepVentas());
        }

        

        private void SubMenuCli_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            CloseAllSubMenus();

            AbrirFormHija(new Clientes(usuarioConectado, OperationDate)); //pa abrir otro form en el panel
        }

        private void btnCliHist_Click(object sender, EventArgs e)
        {
            subMenuReportes.Visible = false;
            AbrirFormHija(new HistorialCli());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            AbrirFormHija(new CheckIn());
        }

        private void btnReservacion_Click(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            subMenuReser.Visible = true;
            AbrirFormHija(new Reservaciones(usuarioConectado, OperationDate));
        }

        private void Checkout_Click(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            AbrirFormHija(new CheckOut());
        }

        private void AbrirFormHija(object formhija) //funcion para abrir form en el panel
        {
            if (this.PanelContenedor.Controls.Count > 0)
                this.PanelContenedor.Controls.RemoveAt(0); //este if quita lo del panel si es q hay algo
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.PanelContenedor.Controls.Add(fh);
            this.PanelContenedor.Tag = fh;
            fh.Show();
        }

        private void PanelContenedor_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new inicio());
        }

        private void button6_Click(object sender, EventArgs e) //es el boton del menu de hotel no le he cambiado el nombre srry
        {
            CloseAllSubMenus();
            submenuH.Visible = true;
            AbrirFormHija(new RegHotel(usuarioConectado, OperationDate));
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            AbrirFormHija(new RegHab(usuarioConectado, OperationDate));
        }

       

        private void button10_Click(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            AbrirFormHija(new Cancelaciones(usuarioConectado, OperationDate));
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            AbrirFormHija(new Empleados(usuarioConectado, OperationDate));
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            AbrirFormHija(new Facturas());
        }

        private void btnReportes_Click_1(object sender, EventArgs e)
        {
            CloseAllSubMenus();
            subMenuReportes.Visible = true;
        }


        private void CloseAllSubMenus()
        {
            //SubMenuCli.Visible = false;
            submenuH.Visible = false;
            subMenuReser.Visible = false;
            subMenuReportes.Visible = false;
        }

        private void menuVertical_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void menuBlanckClicked(object sender, EventArgs e)
        {
            CloseAllSubMenus();
        }

        private void windowBlanckClicked(object sender, EventArgs e)
        {
            CloseAllSubMenus();
        }

        private void headerClicked(object sender, EventArgs e)
        {
            CloseAllSubMenus();
        }
    }
}
