using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateOfficeWebApp.Models
{
    public class Task
    {
        [Key]
        public int IdTask { get; set; }

        [ForeignKey("Ticket")]
        public int? IdTicket { get; set; }
        [ForeignKey("ControlMeasures")]
        public int IdControlMeasures { get; set; }
        public string ContentTask { get; set; }
        public int CountTask { get; set; }
        [Column(TypeName = "real")]
        public double Point { get; set; }
        public virtual ControlMeasures ControlMeasures { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
