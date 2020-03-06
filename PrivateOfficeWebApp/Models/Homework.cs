using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeWebApp.Models
{
    public class Homework
    {

        [Key]
        public int IdHomework { get; set; }
        [ForeignKey("Student")]
        public int? IdStudent { get; set; }
        [ForeignKey("Classes")]
        public int? IdClasses { get; set; }
        [ForeignKey("Group")]
        public int? IdGroup { get; set; }
        public string ContentHomework { get; set; }
        public string TypeHomework { get; set; }
        public virtual Group Group { get; set; }
        public virtual Student Student { get; set; }
        public virtual Classes Classes { get; set; }

    }
}
