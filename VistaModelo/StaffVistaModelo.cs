using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios;
using System.Windows.Data;
using Modelo;
using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Formatting;

namespace VistaModelo
{
    public class StaffVistaModelo : INotifyPropertyChanged
    {
        public StaffModelo StaffModelo { get; set; }
        private ConsultanteMgr consultanteMgr { get; set; }
        public ICollectionView Turnos { get; set; }
        public ICollectionView Puestos { get; set; }

        public StaffVistaModelo()
        {
            this.StaffModelo = new StaffModelo();
            this.consultanteMgr = new ConsultanteMgr();
            ObtenerPuestos();
            ObtenerTurnos();
        }
        private void ObtenerPuestos()
        {
           // if (Sesion.MiembroEnLinea)
            {
                HttpResponseMessage respuesta = this.consultanteMgr.ObtenerPuestos();
                if (!respuesta.IsSuccessStatusCode)
                {
                    string jsonContent = respuesta.Content.ReadAsStringAsync().Result;
                    dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);
                    MessageBox.Show(responseObject["mensaje"].ToString());
                }
                else
                {
                    ObservableCollection <PuestoModelo>  puestosEncontrados=
                        respuesta.Content.ReadAsAsync<ObservableCollection<PuestoModelo>>().Result;
                    CollectionViewSource viewSource = new CollectionViewSource();
                    viewSource.Source = puestosEncontrados;
                    this.Puestos = viewSource.View;

                }
            }
        }
        private void ObtenerTurnos()
        {
            // if (Sesion.MiembroEnLinea)
            {
                HttpResponseMessage respuesta = this.consultanteMgr.ObtenerTurnos();
                if (!respuesta.IsSuccessStatusCode)
                {
                    string jsonContent = respuesta.Content.ReadAsStringAsync().Result;
                    dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);
                    MessageBox.Show(responseObject["mensaje"].ToString());
                }
                else
                {
                    ObservableCollection<TurnoModelo> turnosEncontrados =
                        respuesta.Content.ReadAsAsync<ObservableCollection<TurnoModelo>>().Result;
                    CollectionViewSource viewSource = new CollectionViewSource();
                    viewSource.Source = turnosEncontrados;
                    this.Turnos = viewSource.View;

                }
            }
            
        }

        public Boolean RegistrarStaff(PuestoModelo puestoSeleccionado, TurnoModelo turnoSeleccionado, string contrasena)
        {
            this.StaffModelo.IdPuesto = puestoSeleccionado.Id;
            this.StaffModelo.IdTurno = turnoSeleccionado.Id;
            this.StaffModelo.Contrasena = contrasena;
            // if (Sesion.MiembroEnLinea)
            {
                HttpResponseMessage respuesta = this.consultanteMgr.RegistrarStaff(this.StaffModelo);
                if (!respuesta.IsSuccessStatusCode)
                {
                    string jsonContent = respuesta.Content.ReadAsStringAsync().Result;
                    dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);
                    MessageBox.Show(responseObject["mensaje"].ToString());
                    return false;
                }
                else
                {
                    MessageBox.Show("Miembro agregado al staff, recuerda nuestro lema: " + "¡TODOS SOMOS FAMILIA! :D",
                        "Nuevo integrante agregado", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
