using Medicine.Common;
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
        public string FullName => $"{this.LastName} {this.FirstName} {this.MiddleName}";
        public int Age => DateTime.Now.Year - this.BirthDate.Year;

        public string Gender
        {
            get
            {
                switch (this.GenderId)
                {
                    case 0: return "Мужчина";
                    case 1: return "Женщина";
                }

                return "Не указан";
            }
        }

        public byte[] PhotoExt
        {
            get
            {
                if (this.Photo != null)
                    return this.Photo;
                else
                {
                    using (var stream = new MemoryStream())
                    {
                        Properties.Resources.nophoto.Save(stream, Properties.Resources.nophoto.RawFormat);
                        return stream.ToArray();
                    }
                }
            }
        }
    }
}
