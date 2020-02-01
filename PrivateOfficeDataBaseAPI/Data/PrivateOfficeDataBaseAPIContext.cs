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

        public Microsoft.EntityFrameworkCore.DbSet<TypeClasses> TypeClasses { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Group> Group { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Student> Student { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Report> Report { get; set; }

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

            /*связь один к одному между Classes and TypeClasses*/
            modelBuilder.Entity<Classes>()
                .HasOne(typeClasses => typeClasses.TypeClasses)
                .WithOne(classes => classes.Classes)
                .IsRequired()
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

            modelBuilder.Entity<Classes>()
                .HasMany(report => report.Report)
                .WithOne(classes => classes.Classes)
                .HasForeignKey(report => report.IdClasses);

            base.OnModelCreating(modelBuilder);
        }
    }
}
