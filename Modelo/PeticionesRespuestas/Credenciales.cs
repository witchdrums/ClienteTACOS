using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Modelo.PeticionesRespuestas
{
    public class Credenciales : INotifyPropertyChanged
    {
        private MiembroModelo miembro = null;
        public MiembroModelo Miembro 
        {
            get { return this.miembro; }
            set
            { 
                this.miembro = value;
                this.EsMiembro = (value != null);
                this.OnPropertyChanged();
            }
        }

        private bool esMiembro = false;
        public bool EsMiembro
        {
            get { return this.esMiembro; }
            set
            {
                this.esMiembro = value;
                if (value)
                { 
                    this.VisibilidadMiembro = Visibility.Visible; 
                    this.VisibilidadConsultante = Visibility.Collapsed;
                }
                else
                {
                    this.VisibilidadMiembro = Visibility.Collapsed;
                    this.VisibilidadConsultante = Visibility.Visible;
                }
                this.OnPropertyChanged();
            }
        }

        //Bindea esta propiedad con todos los elementos GUI que sean exclusivos del miembro.
        private Visibility visibilidadMiembro = Visibility.Collapsed;
        public Visibility VisibilidadMiembro
        {
            get { return this.visibilidadMiembro; }
            set
            {
                this.visibilidadMiembro = value;
                this.OnPropertyChanged();
            }
        }

        private Visibility visibilidadConsultante = Visibility.Visible;
        public Visibility VisibilidadConsultante
        {
            get { return this.visibilidadConsultante; }
            set
            {
                this.visibilidadConsultante = value;
                this.OnPropertyChanged();
            }
        }
        public string Token { get; set; }
        public string Expera { get; set; }
        public int Codigo { get; set; }
        public string Mensaje { get; set; }

        public bool ValidarMiembroLoggeado()
        {
            return this.EsMiembro =
                !(this.Miembro is null)
                && (this.Miembro.Id > 0)
                && (this.Miembro.CodigoConfirmacion == 0);
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
