using Medicine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data.Entities
{
    public partial class Item
    {
        public string TypeName => ItemType.List.Find(x => x.Id == this.TypeId)?.Name;
    }
}
