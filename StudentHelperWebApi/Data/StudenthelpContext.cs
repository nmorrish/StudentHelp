#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudentHelperWebApi.Data.Entities;

#nullable disable

namespace StudentHelperWebApi.Data
{
    public partial class StudenthelpContext : DbContext
    {
        public StudenthelpContext() : base()
        {
        }

        public StudenthelpContext(DbContextOptions<StudenthelpContext> options) : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentAssignment> StudentAssignment { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.Property(e => e.AssignmentId).HasColumnName("Assignment_Id");

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignment_Course");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.Name, "UC_CourseName")
                    .IsUnique();

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("Student_Id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<StudentAssignment>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.AssignmentId }).HasName("PK_StudentAssignment");

                entity.ToTable("Student_Assignment");

                entity.Property(e => e.StudentId).HasColumnName("Student_Id");

                entity.Property(e => e.AssignmentId).HasColumnName("Assignment_Id");

                entity.Property(e => e.DateSubmitted).HasColumnType("datetime");

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.StudentAssignment)
                    .HasForeignKey(d => d.AssignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentAssignment_Assignment");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentAssignment)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentAssignment_Student");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("PK_StudentCourse");

                entity.ToTable("Student_Course");

                entity.Property(e => e.StudentId).HasColumnName("Student_Id");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.CompletionDate).HasColumnType("datetime");

                entity.Property(e => e.EnrolledDate).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Student");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}