﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Modelo
{
    public class AlimentoModelo : INotifyPropertyChanged
    {
        //Columnas
        public int Id { get; set; }

        public string nombre { set; get; }
        public string Nombre
        {
            set
            {
                this.nombre = value;
                OnPropertyChanged();
            }
            get { return this.nombre; }
        }
        private string descripcion;
        public string Descripcion
        {
            set
            {
                this.descripcion = value;
                OnPropertyChanged();
            }
            get { return this.descripcion; }
        }

        private int existencia;
        public int Existencia
        {
            set
            {
                this.existencia = value;
                this.Disponible = this.existencia > 0;
                OnPropertyChanged();
            }
            get { return this.existencia; }
        }
        public Imagen Imagen { set; get; }
        public int IdImagen { set; get; }
        public double Precio { set; get; }
        public int IdCategoria { get; set; }
        private int cantidad;
        public int Cantidad
        {
            set
            {
                this.cantidad = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Subtotal));
            }
            get { return this.cantidad; }
        }
        public double Subtotal => this.Cantidad * this.Precio;

        //Otros
        private bool disponible = true;
        public bool Disponible
        {
            get => this.disponible;
            set
            {
                this.disponible = value;
                this.OnPropertyChanged();
            }
        }
        [JsonIgnore]
        public bool Actualizado { set; get; } = false;
        [JsonIgnore]
        public BitmapSource ImagenConvertida => byteArrayToImage();
        public BitmapSource byteArrayToImage()
        {
            Bitmap bitmap;
            using (var stream = new MemoryStream(Imagen.ImagenBytes))
            {
                bitmap = new Bitmap(stream);
            }
            var puntero = bitmap.GetHbitmap();

            return Imaging.CreateBitmapSourceFromHBitmap(puntero, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (!name.Equals(nameof(this.Cantidad)))
            {
                this.Actualizado = true;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
