using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeWebApp.Models
{
    public class PlanClasses
    {
        [Key]
        public int IdPlanClasses { get; set; }
        [ForeignKey("Classes")]
        public int IdClasses { get; set; }
        public string Theme { get; set; }
        public string Poll { get; set; }
        public string Block { get; set; }
        public virtual Classes Classes { get; set; }
    }
}
