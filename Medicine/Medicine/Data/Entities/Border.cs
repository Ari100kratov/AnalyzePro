using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data.Entities
{
    public partial class Border
    {
        public int Id { get; set; }
        public decimal WarningMin { get; set; }
        public decimal WarningMax { get; set; }
        public decimal NormalMin { get; set; }
        public decimal NormalMax { get; set; }
        public int? WarningItem { get; set; }
        public int? NormalItem { get; set; }

        public int? TargetId { get; set; }
        public virtual Target Target { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
