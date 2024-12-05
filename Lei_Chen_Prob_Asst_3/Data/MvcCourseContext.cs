using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PA3.Models;

namespace Lei_Chen_Prob_Asst_3.Data
{
  public class MvcCourseContext : DbContext
  {
    public MvcCourseContext(DbContextOptions<MvcCourseContext> options)
        : base(options)
    {
    }

    public DbSet<PA3.Models.Course> Courses { get; set; } = default!;
    public DbSet<PA3.Models.City> Cities { get; set; } = default!;
    public DbSet<PA3.Models.Province> Provinces { get; set; } = default!;
    public DbSet<PA3.Models.StudentType> StudentTypes { get; set; } = default!;
    public DbSet<PA3.Models.Term> Terms { get; set; } = default!;
    public DbSet<PA3.Models.UGProgram> UGPrograms { get; set; } = default!;

    public DbSet<Student> Students { get; set; } = default!;
    public DbSet<FinancialStatement> FinancialStatements { get; set; }
    public DbSet<StatementEntry> StatementEntries { get; set; }
    public DbSet<FeePolicy> FeePolicies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Configure StudentID for auto-increment starting at 101100
      // first manunally set start id number: UPDATE SQLITE_SEQUENCE SET seq = 101099 WHERE name = 'Students';
      modelBuilder.Entity<Student>()
          .Property(s => s.StudentID)
          .ValueGeneratedOnAdd();

      // Configure many-to-many relationship between Students and Courses
      modelBuilder.Entity<Student>()
          .HasMany(s => s.Courses)
          .WithMany()
          .UsingEntity(j => j.ToTable("StudentCourses"));

      // Configure one-to-many relationship between Program and Students
      modelBuilder.Entity<Student>()
          .HasOne(s => s.Program)
          .WithMany()
          .HasForeignKey(s => s.ProgramID)
          .IsRequired();

      // Configure one-to-many relationship between City and Students
      modelBuilder.Entity<Student>()
          .HasOne(s => s.City)
          .WithMany()
          .HasForeignKey(s => s.CityID)
          .IsRequired();

      // Configure one-to-one relationship between Student and FinancialStatement
      modelBuilder.Entity<Student>()
          .HasOne(s => s.FinancialStatement)
          .WithOne(f => f.Student)
          .HasForeignKey<FinancialStatement>(f => f.StudentID)
          .IsRequired();

      // Configure one-to-many relationship between FinancialStatement and StatementEntry
      modelBuilder.Entity<StatementEntry>()
          .HasOne(se => se.FinancialStatement)
          .WithMany(fs => fs.Entries)
          .HasForeignKey(se => se.FinancialStatementID)
          .IsRequired();

      // Configure many-to-one relationship between FinancialStatement and FeePolicy
      modelBuilder.Entity<FinancialStatement>()
          .HasOne(fs => fs.FeePolicy)
          .WithMany(fp => fp.FinancialStatements)
          .HasForeignKey(fs => fs.FeePolicyID)
          .IsRequired();

      base.OnModelCreating(modelBuilder);
    }
  }
}
