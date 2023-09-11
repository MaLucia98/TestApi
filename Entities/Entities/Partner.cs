using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Partner
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PartnerTypeId { get; set; }
        [ForeignKey("PartnerTypeId")]
        public virtual PartnerTypes? PartnerType { get; set; }
        public virtual ICollection<PartnerSubject>? PartnerSubject { get; set; }
    }
}
