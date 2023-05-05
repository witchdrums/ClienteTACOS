using Modelo;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaModelo
{
    public class MiembroVistaModelo
    {
        public MiembroModelo MiembroModelo { get; set; }
        private ConsultanteMgr consultanteMgr { get; set; }

        public MiembroVistaModelo()
        { 
            this.MiembroModelo = new MiembroModelo();
            this.consultanteMgr = new ConsultanteMgr();
        }

        public void RegistrarMiembro(string contrasena)
        {
            this.MiembroModelo.contrasena = contrasena;
            this.MiembroModelo.pedidosPagados = 0;
            this.consultanteMgr.RegistrarMiembro(this.MiembroModelo);
        }

        public void IniciarSesion(string email, string contrasena)
        {
            MiembroModelo miembroObtenido = this.consultanteMgr.IniciarSesion(email, contrasena);
            Sesion.Miembro = miembroObtenido;
        }
    }
}
