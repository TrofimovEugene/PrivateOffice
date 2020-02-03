using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeDataBaseAPI.Models
{
    public class Questions
    {
        [Key]
        public int IdQuestions { get; set; }

        [ForeignKey("Ticket")]
        public int IdTicket { get; set; }
        [ForeignKey("ControlMeasures")]
        public int IdControlMeasures { get; set; }
        public string ContentQuestions { get; set; }
        public int CountQuestions { get; set; }
        public double Point { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual ControlMeasures ControlMeasures { get; set; }
    }
}
