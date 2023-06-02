using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.PeticionesRespuestas
{
    public class Credenciales : INotifyPropertyChanged
    {
        public MiembroModelo Miembro { get; set; }
        private bool loggeado = false;
        public bool Loggeado
        {
            get { return this.loggeado; }
            set
            {
                this.loggeado = value;
                this.OnPropertyChanged();
            }
        }

        public string Token { get; set; }
        public string Expera { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
