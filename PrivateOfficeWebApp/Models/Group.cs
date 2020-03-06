using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrivateOfficeWebApp.Models
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
