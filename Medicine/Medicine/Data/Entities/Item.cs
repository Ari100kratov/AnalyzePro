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

        public int TemplateId { get; set; }
        public Template Template { get; set; }
    }
}
