using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data.Entities
{
    public partial class Patient
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        public int GenderId { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string OtherPhoneNumber { get; set; }
        public string RegAddress { get; set; }
        public string ResAddress { get; set; }
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        public virtual List<History> Histories { get; set; }
    }
}
