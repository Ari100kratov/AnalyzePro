using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data.Entities
{
    public partial class Target
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int TemplateId { get; set; }

        public virtual Template Template { get; set; }
        public virtual List<Border> Borders { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        public bool IsEnabled { get; set; }
    }
}
