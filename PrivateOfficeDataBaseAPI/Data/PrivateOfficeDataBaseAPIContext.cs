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


            base.OnModelCreating(modelBuilder);
        }
    }
}
