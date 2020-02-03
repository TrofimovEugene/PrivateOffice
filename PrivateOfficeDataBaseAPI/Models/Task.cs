using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeDataBaseAPI.Models
{
    public class Task
    {
        [Key]
        public int IdQuestions { get; set; }

        [ForeignKey("Ticket")]
        public int IdTicket { get; set; }
        [ForeignKey("ControlMeasures")]
        public int IdControlMeasures { get; set; }
        public string TaskName { get; set; }
        public int CountTask { get; set; }
        public double Point { get; set; }
        public virtual ControlMeasures ControlMeasures { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
