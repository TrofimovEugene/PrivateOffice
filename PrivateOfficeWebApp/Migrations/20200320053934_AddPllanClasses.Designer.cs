﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateOfficeWebApp.Data;

namespace PrivateOfficeWebApp.Migrations
{
    [DbContext(typeof(PrivateOfficeWebAppContext))]
    [Migration("20200320053934_AddPllanClasses")]
    partial class AddPllanClasses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Classes", b =>
                {
                    b.Property<int>("IdClasses")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cabinet")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateClasses")
                        .HasColumnType("date");

                    b.Property<string>("DaysWeek")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdCourse")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdGroup")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdTypeClasses")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameClasses")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReplayClasses")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("TEXT");

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
                        .HasColumnType("INTEGER");

                    b.Property<int>("CountControlMeasures")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdClasses")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdStudent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameControlMeasures")
                        .HasColumnType("TEXT");

                    b.HasKey("IdControlMeasures");

                    b.HasIndex("IdClasses");

                    b.HasIndex("IdStudent");

                    b.ToTable("ControlMeasures");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Course", b =>
                {
                    b.Property<int>("IdCourse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CountTime")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<int?>("IdGroup")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdTeacher")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameCourse")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameUniversity")
                        .HasColumnType("TEXT");

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
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameGroup")
                        .HasColumnType("TEXT");

                    b.HasKey("IdGroup");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Homework", b =>
                {
                    b.Property<int>("IdHomework")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentHomework")
                        .HasColumnType("TEXT");

                    b.Property<int?>("IdClasses")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdGroup")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdStudent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeHomework")
                        .HasColumnType("TEXT");

                    b.HasKey("IdHomework");

                    b.HasIndex("IdClasses");

                    b.HasIndex("IdGroup");

                    b.HasIndex("IdStudent");

                    b.ToTable("Homework");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.HomeworkGroup", b =>
                {
                    b.Property<int>("IdHomeworkGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentHomeworkGroup")
                        .HasColumnType("TEXT");

                    b.Property<int?>("IdClasses")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdGroup")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdHomeworkGroup");

                    b.HasIndex("IdClasses")
                        .IsUnique();

                    b.HasIndex("IdGroup");

                    b.ToTable("HomeworkGroup");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.PlanClasses", b =>
                {
                    b.Property<int>("IdPlanClasses")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Block")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdClasses")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Poll")
                        .HasColumnType("TEXT");

                    b.Property<string>("Theme")
                        .HasColumnType("TEXT");

                    b.HasKey("IdPlanClasses");

                    b.HasIndex("IdClasses")
                        .IsUnique();

                    b.ToTable("PlanClasses");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Questions", b =>
                {
                    b.Property<int>("IdQuestions")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentQuestions")
                        .HasColumnType("TEXT");

                    b.Property<int>("CountQuestions")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdControlMeasures")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdTicket")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Point")
                        .HasColumnType("real");

                    b.HasKey("IdQuestions");

                    b.HasIndex("IdControlMeasures");

                    b.HasIndex("IdTicket");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Student", b =>
                {
                    b.Property<int>("IdStudent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdGroup")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecondName")
                        .HasColumnType("TEXT");

                    b.HasKey("IdStudent");

                    b.HasIndex("IdGroup");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Task", b =>
                {
                    b.Property<int>("IdTask")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentTask")
                        .HasColumnType("TEXT");

                    b.Property<int>("CountTask")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdControlMeasures")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdTicket")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Point")
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
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdTeacher");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Ticket", b =>
                {
                    b.Property<int>("IdTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CountTicket")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdControlMeasures")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberTicket")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdTicket");

                    b.HasIndex("IdControlMeasures");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.TypeClasses", b =>
                {
                    b.Property<int>("IdTypeClasses")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeClass")
                        .HasColumnType("TEXT");

                    b.HasKey("IdTypeClasses");

                    b.ToTable("TypeClasses");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.VisitedStudent", b =>
                {
                    b.Property<int>("IdVisitedStudent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ConfirmVisited")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdClasses")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdStudent")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Visited")
                        .HasColumnType("INTEGER");

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
                        .HasForeignKey("IdGroup")
                        .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity("PrivateOfficeWebApp.Models.Homework", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.Classes", "Classes")
                        .WithMany("Homework")
                        .HasForeignKey("IdClasses")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PrivateOfficeWebApp.Models.Group", "Group")
                        .WithMany("Homework")
                        .HasForeignKey("IdGroup");

                    b.HasOne("PrivateOfficeWebApp.Models.Student", "Student")
                        .WithMany("Homework")
                        .HasForeignKey("IdStudent");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.HomeworkGroup", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.Classes", "Classes")
                        .WithOne("HomeworkGroup")
                        .HasForeignKey("PrivateOfficeWebApp.Models.HomeworkGroup", "IdClasses")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PrivateOfficeWebApp.Models.Group", "Group")
                        .WithMany("HomeworkGroup")
                        .HasForeignKey("IdGroup");
                });

            modelBuilder.Entity("PrivateOfficeWebApp.Models.PlanClasses", b =>
                {
                    b.HasOne("PrivateOfficeWebApp.Models.Classes", "Classes")
                        .WithOne("PlanClasses")
                        .HasForeignKey("PrivateOfficeWebApp.Models.PlanClasses", "IdClasses")
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
