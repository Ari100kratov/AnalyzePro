using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data.Entities
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string MeasureUnit { get; set; }
        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }
        public virtual List<CheckList> CheckLists { get; set; }
        public virtual List<Border> Borders { get; set; }
    }
}
