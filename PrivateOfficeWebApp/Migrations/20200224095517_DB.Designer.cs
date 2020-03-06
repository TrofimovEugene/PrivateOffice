﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateOfficeWebApp.Data;

namespace PrivateOfficeWebApp.Migrations
{
    [DbContext(typeof(PrivateOfficeWebAppContext))]
    [Migration("20200224095517_DB")]
    partial class DB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Classes", b =>
                {
                    b.Property<int>("IdClasses")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cabinet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateClasses")
                        .HasColumnType("date");

                    b.Property<string>("DaysWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int>("IdCourse")
                        .HasColumnType("int");

                    b.Property<int?>("IdGroup")
                        .HasColumnType("int");

                    b.Property<int>("IdTypeClasses")
                        .HasColumnType("int");

                    b.Property<string>("NameClasses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplayClasses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("IdClasses");

                    b.HasIndex("IdCourse");

                    b.HasIndex("IdGroup");

                    b.HasIndex("IdTypeClasses");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.ControlMeasures", b =>
                {
                    b.Property<int>("IdControlMeasures")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountControlMeasures")
                        .HasColumnType("int");

                    b.Property<int>("IdClasses")
                        .HasColumnType("int");

                    b.Property<int?>("IdStudent")
                        .HasColumnType("int");

                    b.Property<string>("NameControlMeasures")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdControlMeasures");

                    b.HasIndex("IdClasses");

                    b.HasIndex("IdStudent");

                    b.ToTable("ControlMeasures");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Course", b =>
                {
                    b.Property<int>("IdCourse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountTime")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<int?>("IdGroup")
                        .HasColumnType("int");

                    b.Property<int>("IdTeacher")
                        .HasColumnType("int");

                    b.Property<string>("NameCourse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameUniversity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("IdCourse");

                    b.HasIndex("IdGroup");

                    b.HasIndex("IdTeacher");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Group", b =>
                {
                    b.Property<int>("IdGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameGroup")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdGroup");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Questions", b =>
                {
                    b.Property<int>("IdQuestions")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentQuestions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountQuestions")
                        .HasColumnType("int");

                    b.Property<int>("IdControlMeasures")
                        .HasColumnType("int");

                    b.Property<int?>("IdTicket")
                        .HasColumnType("int");

                    b.Property<float>("Point")
                        .HasColumnType("real");

                    b.HasKey("IdQuestions");

                    b.HasIndex("IdControlMeasures");

                    b.HasIndex("IdTicket");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Report", b =>
                {
                    b.Property<int>("IdReport")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdClasses")
                        .HasColumnType("int");

                    b.Property<int?>("IdStudent")
                        .HasColumnType("int");

                    b.Property<string>("NameReport")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdReport");

                    b.HasIndex("IdClasses");

                    b.HasIndex("IdStudent");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Student", b =>
                {
                    b.Property<int>("IdStudent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdGroup")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdStudent");

                    b.HasIndex("IdGroup");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Task", b =>
                {
                    b.Property<int>("IdTask")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentTask")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountTask")
                        .HasColumnType("int");

                    b.Property<int>("IdControlMeasures")
                        .HasColumnType("int");

                    b.Property<int?>("IdTicket")
                        .HasColumnType("int");

                    b.Property<float>("Point")
                        .HasColumnType("real");

                    b.HasKey("IdTask");

                    b.HasIndex("IdControlMeasures");

                    b.HasIndex("IdTicket");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Teacher", b =>
                {
                    b.Property<int>("IdTeacher")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTeacher");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Ticket", b =>
                {
                    b.Property<int>("IdTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountTicket")
                        .HasColumnType("int");

                    b.Property<int>("IdControlMeasures")
                        .HasColumnType("int");

                    b.Property<int>("NumberTicket")
                        .HasColumnType("int");

                    b.HasKey("IdTicket");

                    b.HasIndex("IdControlMeasures");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.TypeClasses", b =>
                {
                    b.Property<int>("IdTypeClasses")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeClass")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTypeClasses");

                    b.ToTable("TypeClasses");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.VisitedStudent", b =>
                {
                    b.Property<int>("IdVisitedStudent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ConfirmVisited")
                        .HasColumnType("bit");

                    b.Property<int?>("IdClasses")
                        .HasColumnType("int");

                    b.Property<int?>("IdStudent")
                        .HasColumnType("int");

                    b.Property<bool>("Visited")
                        .HasColumnType("bit");

                    b.HasKey("IdVisitedStudent");

                    b.HasIndex("IdClasses");

                    b.HasIndex("IdStudent");

                    b.ToTable("VisitedStudents");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Classes", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.Course", "Course")
                        .WithMany("Classes")
                        .HasForeignKey("IdCourse")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrivateOfficeWebApp.Models.Group", "Group")
                        .WithMany("Classes")
                        .HasForeignKey("IdGroup");

                    b.HasOne("PrivateOfficeWebApp.Models.TypeClasses", "TypeClasses")
                        .WithMany("Classes")
                        .HasForeignKey("IdTypeClasses")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.ControlMeasures", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.Classes", "Classes")
                        .WithMany("ControlMeasures")
                        .HasForeignKey("IdClasses")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrivateOfficeWebApp.Models.Student", "Student")
                        .WithMany("ControlMeasures")
                        .HasForeignKey("IdStudent");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Course", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.Group", "Group")
                        .WithMany("Course")
                        .HasForeignKey("IdGroup")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PrivateOfficeWebApp.Models.Teacher", "Teacher")
                        .WithMany("Course")
                        .HasForeignKey("IdTeacher")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Questions", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.ControlMeasures", "ControlMeasures")
                        .WithMany("Questions")
                        .HasForeignKey("IdControlMeasures")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrivateOfficeWebApp.Models.Ticket", "Ticket")
                        .WithMany("Questions")
                        .HasForeignKey("IdTicket");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Report", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.Classes", "Classes")
                        .WithMany("Report")
                        .HasForeignKey("IdClasses")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PrivateOfficeWebApp.Models.Student", "Student")
                        .WithMany("Report")
                        .HasForeignKey("IdStudent");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Student", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.Group", "Group")
                        .WithMany("Student")
                        .HasForeignKey("IdGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Task", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.ControlMeasures", "ControlMeasures")
                        .WithMany("Task")
                        .HasForeignKey("IdControlMeasures")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrivateOfficeWebApp.Models.Ticket", "Ticket")
                        .WithMany("Task")
                        .HasForeignKey("IdTicket");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Ticket", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.ControlMeasures", "ControlMeasures")
                        .WithMany("Ticket")
                        .HasForeignKey("IdControlMeasures")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.VisitedStudent", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.Classes", "Classes")
                        .WithMany("VisitedStudents")
                        .HasForeignKey("IdClasses")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PrivateOfficeWebApp.Models.Student", "Student")
                        .WithMany("VisitedStudents")
                        .HasForeignKey("IdStudent");
                });
#pragma warning restore 612, 618
        }
    }
}
