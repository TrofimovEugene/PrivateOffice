using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeDataBaseAPI.Models
{
    public class ControlMeasures
    {
        [Key]
        public int IdControlMeasures { get; set; }
          [ForeignKey("Classes")]
        public int IdClasses { get; set; }
          [ForeignKey("Student")]
        public int IdStudent { get; set; }
        public string NameControlMeasures { get; set; }
        public int CountControlMeasures { get; set; }
        public virtual ICollection<Task> Task { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }

    }
}
