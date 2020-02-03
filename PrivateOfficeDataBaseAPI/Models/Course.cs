using System;
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
        [ForeignKey("Group")]
        public int? IdGroup { get; set; }

        [ForeignKey("Teacher")]
        public int IdTeacher { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        public int CountTime { get; set;}
        
        public string NameUniversity { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Group Group { get; set; }
   
        public virtual ICollection<Classes> Classes { get; set; }



    }
}
