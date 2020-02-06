using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using PrivateOfficeDataBaseAPI.Models;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace PrivateOfficeDataBaseAPI.Data
{
    public class PrivateOfficeDataBaseAPIContext : DbContext
    {
        public PrivateOfficeDataBaseAPIContext (DbContextOptions<PrivateOfficeDataBaseAPIContext> options)
            : base(options)
        { }
        /*сущности модели*/
		public Microsoft.EntityFrameworkCore.DbSet<Teacher> Teacher { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Course> Course { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Classes> Classes { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Group> Group { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Student> Student { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Report> Report { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<ControlMeasures> ControlMeasures { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Ticket> Ticket { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Task> Task { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Questions> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        

            /*связь один ко многим между Teacher and Course*/
            modelBuilder.Entity<Teacher>()
                .HasMany(course => course.Course)
                .WithOne(teacher => teacher.Teacher)
                .IsRequired().HasForeignKey(course => course.IdTeacher)
                .OnDelete(DeleteBehavior.Cascade);

            /*связь один ко многим между Course and Classes*/
            modelBuilder.Entity<Course>()
                .HasMany(classes => classes.Classes)
                .WithOne(course => course.Course)
                .IsRequired()
                .HasForeignKey(course => course.IdCourse)
                .OnDelete(DeleteBehavior.Cascade);


            /*связь один ко многим между Classes and Group*/
            modelBuilder.Entity<Group>()
                .HasMany(classes => classes.Classes)
                .WithOne(group => group.Group)
                .IsRequired()
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
