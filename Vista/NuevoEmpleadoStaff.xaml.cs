using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Modelo;
using VistaModelo;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Vista
{
    public partial class NuevoEmpleadoStaff : Page
    {
        private StaffVistaModelo staffVistaModelo;

        public NuevoEmpleadoStaff()
        {
            InitializeComponent();
            this.staffVistaModelo = new StaffVistaModelo();
        }

        private void IntegrarAlStaff(object sender, RoutedEventArgs e)
        { 
            if (ValidarPuestoyTurnoSeleccionado())
            {
                PuestoModelo puestoSeleccionado = this.ComboBox_Puestos.SelectedItem as PuestoModelo;
                TurnoModelo turnoSeleccionado = this.ComboBox_Turnos.SelectedItem as TurnoModelo;
                StaffVistaModelo staffRegistrar = this.DataContext as StaffVistaModelo;
                if (staffRegistrar.RegistrarStaff(puestoSeleccionado, turnoSeleccionado, this.PasswordBox.Password))
                {
                    this.LimpiarCampos();
                }
            }
        }

        private Boolean ValidarPuestoyTurnoSeleccionado()
        {
            if (this.ComboBox_Puestos.SelectedItem == null || this.ComboBox_Turnos.SelectedItem == null)
            {
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            this.TextBox_Email.Clear();
            this.PasswordBox.Clear();
            this.TextBox_Nombre.Clear();
            this.TextBox_ApellidoPaterno.Clear();
            this.TextBox_ApellidoMaterno.Clear();
            this.TextBox_Direccion.Clear();
            this.TextBox_Telefono.Clear();
        }
    }
}
