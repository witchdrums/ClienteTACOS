using Modelo;
using Modelo.PeticionesRespuestas;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public sealed class Sesion : INotifyPropertyChanged
    {
        private static readonly Sesion instancia = new Sesion();

        private Credenciales credenciales = new Credenciales();
        public Credenciales Credenciales
        {
            get => this.credenciales;
            set
            {
                if (value != null)
                {
                    this.credenciales = value;
                    this.EsMiembro = this.credenciales.EsMiembro;
                    this.EsAsociado = this.credenciales.EsMiembro || this.credenciales.EsStaff;
                    this.EsConsultante = !this.EsAsociado;
                    if (this.Credenciales.EsStaff)
                    {
                        this.EsGerente = this.credenciales.Staff.IdPuesto < 2;
                        this.EsCajero = this.credenciales.Staff.IdPuesto < 3;
                    }
                }
            }
        }
        public bool MiembroConfirmado => this.Credenciales.Miembro.CodigoConfirmacion == 0;
        public bool MiembroEnLinea => 
            this.Credenciales.Miembro != null && !String.IsNullOrEmpty(this.Credenciales.Token);

        private bool esConsultante = true;
        public bool EsConsultante 
        {
            get => this.esConsultante;
            set
            {
                this.esConsultante = value;
                this.OnPropertyChanged();
            }
        }

        private bool esMiembro = false;
        public bool EsMiembro
        {
            get => this.esMiembro;
            set
            {
                this.esMiembro = value;
                this.OnPropertyChanged();
            }
        }

        private bool esAsociado = false;
        public bool EsAsociado
        {
            get => this.esAsociado;
            set
            {
                this.esAsociado = value;
                this.OnPropertyChanged();
            }
        }

        private bool esCajero = false;
        public bool EsCajero
        {
            get => this.esCajero;
            set
            {
                this.esCajero = value;
                this.OnPropertyChanged();
            }
        }

        private bool esGerente = false;
        public bool EsGerente
        {
            get => this.esGerente;
            set
            {
                this.esGerente = value;
                this.OnPropertyChanged();
            }
        }

        public static ObservableCollection<AlimentoPedidoModelo> AlimentosPedidos { get; set; } = 
            new ObservableCollection<AlimentoPedidoModelo>();
        static Sesion()
        {
        }

        private Sesion()
        {
        }

        public static Sesion Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void RevocarPermisos()
        {

            this.EsConsultante = true;
            this.EsMiembro = false;
            this.EsAsociado = false;
            this.EsCajero = false;
            this.EsGerente = false;
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
