using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data.Entities
{
    public partial class TemplateGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Template> Templates { get; set; }
    }
}
