using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class PartnerSubject
    {
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject? Subject { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Partner? Student { get; set; }
    }
}