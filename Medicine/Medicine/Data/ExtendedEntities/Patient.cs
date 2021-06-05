using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Medicine.Data.Entities
{
    public partial class Patient
    {
        public int Age => DateTime.Now.Year - this.BirthDate.Year;

        public BitmapImage PhotoImg
        {
            get
            {
                MemoryStream stream = new MemoryStream();
                stream.Write(this.Photo, 0, this.Photo.Length);
                stream.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage returnImage = new BitmapImage();
                returnImage.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                returnImage.StreamSource = ms;
                returnImage.EndInit();

                return returnImage;
            }
        }
    }
}
