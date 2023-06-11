using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VistaModelo;
using Modelo;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Vista
{
    /// <summary>
    /// Interaction logic for DialogoEntrada.xaml
    /// </summary>
    public partial class Confirmacion : Window
    {
        public Confirmacion(MiembroVistaModelo vistaModelo)
        {
            InitializeComponent();
            this.DataContext = vistaModelo;
        }

        private void EnviarCodigo(object sender, RoutedEventArgs e)
        {
            MiembroModelo miembro;
            if (Sesion.Instancia.Credenciales.Miembro.Persona.Id == 0)
            {
                miembro = new MiembroModelo();
            }
            else
            {
                miembro = Sesion.Instancia.Credenciales.Miembro;
            }
            miembro.CodigoConfirmacion = Int32.Parse(this.TextBox_Codigo.Text);
            try
            {
                (this.DataContext as MiembroVistaModelo).EnviarCodigoConfirmacion(miembro);
                Sesion.Instancia.RevocarPermisos();
                this.TextBlock_Cabecera.Text = "¡Confirmado!";
                this.TextBlock_Contenido.Text = "Ya puedes entrar con tu email y contraseña.";
                this.TextBox_Codigo.Visibility = Visibility.Hidden;
                this.Button_Salir.Content="Continuar";
                this.Button_Enviar.Visibility = Visibility.Collapsed;
            }
            catch (HttpRequestException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }
        private void ValidacionNumerica(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }


        private void Salir(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Cancelar"))
            {
                Sesion.Instancia.Credenciales = null;
            }
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Sesion.Instancia.Credenciales != null && !Sesion.Instancia.MiembroConfirmado)
            {
                Sesion.Instancia.Credenciales = null;
            }
        }
    }
}
