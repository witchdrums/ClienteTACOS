using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Modelo
{
    public class ResenaModelo : INotifyPropertyChanged
    {
        //Columnas
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMiembro { get; set; }
        public MiembroModelo Miembro { get; set; }

        //Otros
        public BitmapSource ImagenConvertida => byteArrayToImage();


        public BitmapSource byteArrayToImage()
        {
            if (Imagen != null)
            {
                Bitmap bitmap;
                using (var stream = new MemoryStream(Imagen))
                {
                    bitmap = new Bitmap(stream);
                }
                var puntero = bitmap.GetHbitmap();
                return Imaging.CreateBitmapSourceFromHBitmap(puntero, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            return null;

        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
