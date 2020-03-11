using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeWebApp.Models
{
    public class HomeworkGroup
    {

        [Key]
        public int IdHomeworkGroup { get; set; }
        [ForeignKey("Classes")]
        public int? IdClasses { get; set; }
        [ForeignKey("Group")]
        public int? IdGroup { get; set; }
        public string ContentHomeworkGroup { get; set; }
      
        public virtual Group Group { get; set; }

        public virtual Classes Classes { get; set; }
    }
}
