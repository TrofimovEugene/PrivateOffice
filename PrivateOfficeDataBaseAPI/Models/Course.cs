using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateOfficeDataBaseAPI.Models
{
    public class Course
    {
        [Key] 
        public int IdCourse { get; set; }
        public string NameCourse { get; set; }
        
        [ForeignKey("Teacher")]
        public int IdTeacher { get; set; }
        public virtual Teacher Teacher { get; set; }
   
        public virtual ICollection<Classes> Classes { get; set; }



    }
}
