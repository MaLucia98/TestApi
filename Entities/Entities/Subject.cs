using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Credits { get; set; }
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Partner Teacher { get; set; }
        public virtual ICollection<PartnerSubject> StudentsSubject { get; set; }
    }
}
