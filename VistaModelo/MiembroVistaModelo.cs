using Modelo;
using Modelo.PeticionesRespuestas;
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
            this.MiembroModelo.Contrasena = contrasena;
            this.MiembroModelo.PedidosPagados = 0;
            Sesion.Instancia.Credenciales.Miembro = this.consultanteMgr.RegistrarMiembro(this.MiembroModelo);
        }

        public async Task IniciarSesion(PeticionCredenciales peticionCredenciales)
        {
            
            Sesion.Instancia.Credenciales = await this.consultanteMgr.IniciarSesion(peticionCredenciales);
        }

        public bool EnviarCodigoConfirmacion(MiembroModelo persona)
        {
            return this.consultanteMgr.ConfirmarRegistro(persona).Codigo == 200;
            
        }
    }
}
