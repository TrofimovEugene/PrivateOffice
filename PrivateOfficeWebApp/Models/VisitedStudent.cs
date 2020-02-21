using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeWebApp.Models
{
    public class VisitedStudent
    {
        [Key]
        public int IdVisitedStudent { get; set; }
        [ForeignKey("Student")]
        public int? IdStudent { get; set; }
        [ForeignKey("Classes")]
        public int? IdClasses { get; set; }
        public bool Visited { get; set; }
        public bool ConfirmVisited { get; set; }
        public virtual Student Student { get; set; }
        public virtual Classes Classes { get; set; }
    }
}
