﻿using Modelo;
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
        public PersonaModelo MiembroModelo { get; set; }
        private ConsultanteMgr consultanteMgr { get; set; }



        public MiembroVistaModelo()
        { 
            this.MiembroModelo = new PersonaModelo();
            this.MiembroModelo.Miembros.Add(new MiembroModelo());
            this.consultanteMgr = new ConsultanteMgr();
        }

        public void RegistrarMiembro(string contrasena)
        {
            this.MiembroModelo.Miembros[0].Contrasena = contrasena;
            this.MiembroModelo.Miembros[0].PedidosPagados = 0;
            Sesion.Credenciales = new Modelo.PeticionesRespuestas.Credenciales();
            Sesion.Credenciales.Miembro = this.consultanteMgr.RegistrarMiembro(this.MiembroModelo);
        }

        public void IniciarSesion(string email, string contrasena)
        {
            Sesion.Credenciales = this.consultanteMgr.IniciarSesion(email, contrasena);
        }

        public void EnviarCodigoConfirmacion(MiembroModelo persona)
        {
            this.consultanteMgr.ConfirmarRegistro(persona);
        }
    }
}
