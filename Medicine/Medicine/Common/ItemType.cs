using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Common
{
    public static class ItemType
    {
        public static List<ItemTypeModel> List = new List<ItemTypeModel>()
        {
            new ItemTypeModel{Id=0, Name="Числовое значение"},
            new ItemTypeModel{Id=1, Name = "Список"},
        };

        public static List<ItemTypeModel> GetList()
        {
            return new List<ItemTypeModel>()
            {
                new ItemTypeModel{Id=0, Name="Числовое значение"},
                new ItemTypeModel{Id=1, Name = "Список"},
            };
        }
    }

    public class ItemTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
