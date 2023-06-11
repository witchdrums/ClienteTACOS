using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios;
using System.Windows.Data;
using Modelo;
using System.Windows;
using System.Net.Http;
using ScottPlot.Renderable;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace VistaModelo
{
    public class ResenaVistaModelo : INotifyPropertyChanged
    {
        public ICollectionView Resenas { get; set; }
        private readonly ConsultanteMgr consultanteMgr;
        public ResenaVistaModelo()
        {
            this.consultanteMgr = new ConsultanteMgr();
            this.Resenas = CollectionViewSource.GetDefaultView(consultanteMgr.ObtenerResenas());
        }

        public void BorrarResena(int idResena)
        {
            if (Sesion.Instancia.MiembroEnLinea)
            {
                HttpResponseMessage response = this.consultanteMgr.BorrarResena(idResena);
                if (!response.IsSuccessStatusCode)
                {
                    string jsonContent = response.Content.ReadAsStringAsync().Result;
                    dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);
                    MessageBox.Show(responseObject["mensaje"].ToString());
                }
                else
                {
                    MessageBox.Show("Reseña borrada", "Operaci+on exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
