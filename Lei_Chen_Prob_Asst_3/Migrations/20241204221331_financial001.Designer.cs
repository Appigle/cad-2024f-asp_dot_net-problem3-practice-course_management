﻿// <auto-generated />
using System;
using Lei_Chen_Prob_Asst_3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lei_Chen_Prob_Asst_3.Migrations
{
    [DbContext(typeof(MvcCourseContext))]
    [Migration("20241204221331_financial001")]
    partial class financial001
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CoursesID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentID")
                        .HasColumnType("INTEGER");

                    b.HasKey("CoursesID", "StudentID");

                    b.HasIndex("StudentID");

                    b.ToTable("StudentCourses", (string)null);
                });

            modelBuilder.Entity("PA3.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<int>("ProvinceID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProvinceID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("PA3.Models.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("ProgramID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Section")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Term")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("TermID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProgramID");

                    b.HasIndex("TermID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("PA3.Models.FeePolicy", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("FacilitiesFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RegistrationFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TuitionFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnionFee")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("FeePolicies");
                });

            modelBuilder.Entity("PA3.Models.FinancialStatement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FeePolicyID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastChanged")
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("FeePolicyID");

                    b.HasIndex("StudentID")
                        .IsUnique();

                    b.ToTable("FinancialStatements");
                });

            modelBuilder.Entity("PA3.Models.Province", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("PA3.Models.StatementEntry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("FinancialStatementID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("FinancialStatementID");

                    b.ToTable("StatementEntries");
                });

            modelBuilder.Entity("PA3.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("CityID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<int>("ProgramID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("StudentID");

                    b.HasIndex("CityID");

                    b.HasIndex("ProgramID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("PA3.Models.StudentType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("StudentTypes");
                });

            modelBuilder.Entity("PA3.Models.Term", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("PA3.Models.UGProgram", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("UGPrograms");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("PA3.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PA3.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PA3.Models.City", b =>
                {
                    b.HasOne("PA3.Models.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("PA3.Models.Course", b =>
                {
                    b.HasOne("PA3.Models.UGProgram", "Program")
                        .WithMany("Courses")
                        .HasForeignKey("ProgramID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PA3.Models.Term", null)
                        .WithMany("Courses")
                        .HasForeignKey("TermID");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("PA3.Models.FinancialStatement", b =>
                {
                    b.HasOne("PA3.Models.FeePolicy", "FeePolicy")
                        .WithMany("FinancialStatements")
                        .HasForeignKey("FeePolicyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PA3.Models.Student", "Student")
                        .WithOne("FinancialStatement")
                        .HasForeignKey("PA3.Models.FinancialStatement", "StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FeePolicy");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("PA3.Models.StatementEntry", b =>
                {
                    b.HasOne("PA3.Models.FinancialStatement", "FinancialStatement")
                        .WithMany("Entries")
                        .HasForeignKey("FinancialStatementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FinancialStatement");
                });

            modelBuilder.Entity("PA3.Models.Student", b =>
                {
                    b.HasOne("PA3.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PA3.Models.UGProgram", "Program")
                        .WithMany()
                        .HasForeignKey("ProgramID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("PA3.Models.FeePolicy", b =>
                {
                    b.Navigation("FinancialStatements");
                });

            modelBuilder.Entity("PA3.Models.FinancialStatement", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("PA3.Models.Province", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("PA3.Models.Student", b =>
                {
                    b.Navigation("FinancialStatement");
                });

            modelBuilder.Entity("PA3.Models.Term", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("PA3.Models.UGProgram", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
