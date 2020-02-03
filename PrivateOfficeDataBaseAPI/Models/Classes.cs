using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateOfficeDataBaseAPI.Models
{
    public class Classes
    {
        [Key]
        public int IdClasses { get; set; }
        [ForeignKey("TypeClasses")]
        public int IdTypeClasses { get; set; }
        [ForeignKey("Course")]
        public int IdCourse { get; set; }
        [ForeignKey("Group")]
        public int? IdGroup { get; set; }
        public string NameClasses { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DaysWeek { get; set; }
        public string ReplayClasses { get; set; }

        public virtual Course Course { get; set; }
        public virtual TypeClasses TypeClasses { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Report> Report { get; set; }

        public virtual ICollection<ControlMeasures> ControlMeasures { get; set; }

    }
}
