using Microsoft.EntityFrameworkCore;
using PrivateOfficeWebApp.Models;
using Classes = PrivateOfficeWebApp.Models.Classes;
using Course = PrivateOfficeWebApp.Models.Course;
using Group = PrivateOfficeWebApp.Models.Group;
using Report = PrivateOfficeWebApp.Models.Report;
using Student = PrivateOfficeWebApp.Models.Student;
using Teacher = PrivateOfficeWebApp.Models.Teacher;
using TypeClasses = PrivateOfficeWebApp.Models.TypeClasses;

namespace PrivateOfficeWebApp.Data
{
    public class PrivateOfficeWebAppContext : DbContext
    {
	    public PrivateOfficeWebAppContext(DbContextOptions<PrivateOfficeWebAppContext> options)
		    : base(options)
        { }  
        /*сущности модели*/
		public DbSet<Teacher> Teacher { get; set; }

        public DbSet<Course> Course { get; set; }
        public DbSet<Classes> Classes { get; set; }

        public DbSet<TypeClasses> TypeClasses { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Student> Student { get; set; }

        public DbSet<Report> Report { get; set; }
        public DbSet<VisitedStudent> VisitedStudents { get; set; }

        public DbSet<ControlMeasures> ControlMeasures { get; set; }

        public DbSet<Ticket> Ticket { get; set; }

        public DbSet<Task> Task { get; set; }

        public DbSet<Questions> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        

            /*связь один ко многим между Teacher and Course*/
            modelBuilder.Entity<Teacher>()
                .HasMany(course => course.Course)
                .WithOne(teacher => teacher.Teacher)
                .IsRequired()
               .HasForeignKey(course => course.IdTeacher)
                .OnDelete(DeleteBehavior.Cascade);

            /*связь один ко многим между Course and Classes*/
            modelBuilder.Entity<Course>()
                .HasMany(classes => classes.Classes)
                .WithOne(course => course.Course)
                .HasForeignKey(course => course.IdCourse)
                .OnDelete(DeleteBehavior.Cascade);

            /*связь один к одному между Classes and TypeClasses*/
            modelBuilder.Entity<TypeClasses>()
                .HasMany(classes => classes.Classes)
                .WithOne(typeClasses => typeClasses.TypeClasses)
                .HasForeignKey(classes => classes.IdTypeClasses)
                .OnDelete(DeleteBehavior.Cascade);

            /*связь один ко многим между Classes and Group*/
            modelBuilder.Entity<Group>()
                .HasMany(classes => classes.Classes)
                .WithOne(group => group.Group)
                .HasForeignKey(classes => classes.IdGroup)
                .OnDelete(DeleteBehavior.Cascade);

            /*связь один ко многим между Course and Group*/
            modelBuilder.Entity<Group>()
                .HasMany(course => course.Course)
                .WithOne(group => group.Group)
                .HasForeignKey(course => course.IdGroup);

            /*связь один ко многим между Group and Student*/

            modelBuilder.Entity<Group>()
                .HasMany(student => student.Student)
                .WithOne(group => group.Group)
                .HasForeignKey(student => student.IdGroup);

            /*связь один ко многим между Student and Report*/

            modelBuilder.Entity<Student>()
                .HasMany(report => report.Report)
                .WithOne(student => student.Student)
               .HasForeignKey(report => report.IdStudent);


            /*связь один ко многим между Classes and Report*/
            modelBuilder.Entity<Classes>()
                .HasMany(report => report.Report)
                .WithOne(classes => classes.Classes)
                .HasForeignKey(report => report.IdClasses);

            /*связь один ко многим между ControlMeasures and Classes*/
            modelBuilder.Entity<Classes>()
                .HasMany(controlMeasures => controlMeasures.ControlMeasures)
                .WithOne(classes => classes.Classes)
                .HasForeignKey(controlMeasures => controlMeasures.IdClasses);

            /*связь один ко многим между ControlMeasures and Student*/
            modelBuilder.Entity<Student>()
                .HasMany(controlMeasures => controlMeasures.ControlMeasures)
                .WithOne(student => student.Student)
                .HasForeignKey(controlMeasures => controlMeasures.IdStudent);

            /*связь один ко многим между ControlMeasures and Task*/
            modelBuilder.Entity<ControlMeasures>()
                .HasMany(task => task.Task)
                .WithOne(controlMeasures => controlMeasures.ControlMeasures)
                .HasForeignKey(task => task.IdControlMeasures);

            /*связь один ко многим между ControlMeasures and Ticket*/
            modelBuilder.Entity<ControlMeasures>()
                .HasMany(ticket => ticket.Ticket)
                .WithOne(controlMeasures => controlMeasures.ControlMeasures)
                .HasForeignKey(ticket => ticket.IdControlMeasures);

            /*связь один ко многим между ControlMeasures and Questions*/
            modelBuilder.Entity<ControlMeasures>()
                .HasMany(questions => questions.Questions)
                .WithOne(controlMeasures => controlMeasures.ControlMeasures)
                .HasForeignKey(questions => questions.IdControlMeasures);

            /*связь один ко многим между Ticket and Questions*/
            modelBuilder.Entity<Ticket>()
                .HasMany(questions => questions.Questions)
                .WithOne(ticket => ticket.Ticket)
                .HasForeignKey(questions => questions.IdTicket);

            /*связь один ко многим между Ticket and Task*/
            modelBuilder.Entity<Ticket>()
                .HasMany(task => task.Task)
                .WithOne(ticket => ticket.Ticket)
                .HasForeignKey(task => task.IdTicket);
            modelBuilder.Entity<Student>()
                .HasMany(visitedStudent => visitedStudent.VisitedStudents)
                .WithOne(student => student.Student)
                .HasForeignKey(visitedStudents => visitedStudents.IdStudent);
            modelBuilder.Entity<Classes>()
                .HasMany(visitedStudent => visitedStudent.VisitedStudents)
                .WithOne(classes => classes.Classes)
                .HasForeignKey(visitedStudent => visitedStudent.IdClasses);

            base.OnModelCreating(modelBuilder);
        }
    }
}
