using System;

namespace PrivateOfficeAPI.Models
{
    public class Class
    {
        public int IdClass { get; set; }
        public string NameClass { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int CountTime { get; set; }
        
        public int IdCourse { get; set; }
        public Course Course { get; set; }
    }
}