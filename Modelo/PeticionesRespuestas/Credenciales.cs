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
        private MiembroModelo miembro = new MiembroModelo();
        public MiembroModelo Miembro 
        {
            get { return this.miembro; }
            set
            { 
                this.miembro = value;
                this.OnPropertyChanged();
            }
        }

        public StaffModelo Staff { get; set; } = new StaffModelo();

        public bool EsStaff => this.Staff != null;

        public bool EsMiembro => this.Miembro != null;

        public string Token { get; set; }
        public string Expera { get; set; }
        public int Codigo { get; set; }
        public string Mensaje { get; set; }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
