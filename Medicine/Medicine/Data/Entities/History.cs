using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data.Entities
{
    public partial class History
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int? TargetId { get; set; }
        public virtual Target Target { get; set; }
        public virtual List<ResultData> ResultDatas { get; set; }
    }
}
