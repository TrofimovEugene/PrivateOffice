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

        public string NameGroup { get; set; }
        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Classes> Classes { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
