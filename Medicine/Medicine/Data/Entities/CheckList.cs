using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data.Entities
{
    public partial class CheckList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
