using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeDataBaseAPI.Models
{
    public class Group
    {
        [Key]
        public int IdGroup { get; set; }
        [ForeignKey("Classes")]
        public int IdClasses { get; set; }
        public string NameGroup { get; set; }
        public virtual Classes Classes { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
