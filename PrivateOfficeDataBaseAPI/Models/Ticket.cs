using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeDataBaseAPI.Models
{
    public class Ticket
    {
        [Key]
        public int IdTicket { get; set; }
        public int NumberTicket { get; set; }
        public int CountTicket { get; set; }
        [ForeignKey("ControlMeasures")]
        public int IdControlMeasures { get; set; }
        public virtual ControlMeasures ControlMeasures { get; set; }
        public virtual ICollection<Task> Task { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }

    }
}
