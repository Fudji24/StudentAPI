using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace StudentAPI.Models
{
    public partial class StudentDBContext : DbContext
    {
        public StudentDBContext()
        {
        }

        public StudentDBContext(DbContextOptions<StudentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kurs> Kurs { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentKurs> StudentKurs { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kurs>(entity =>
            {
                entity.Property(e => e.KursId).HasColumnName("KursID");

                entity.Property(e => e.KursName)
                    .IsRequired()
                    .HasMaxLength(100);

               
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.StudentStatusId);

                entity.Property(e => e.StudentStatusId).HasColumnName("StudentStatusID");

                entity.Property(e => e.Status1)
                    .IsRequired()
                    .HasColumnName("Status")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IndexNum)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

               

            });

            modelBuilder.Entity<StudentKurs>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.KursId });

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.KursId).HasColumnName("KursID");

                entity.HasOne(d => d.Kurs)
                    .WithMany(p => p.StudentKurs)
                    .HasForeignKey(d => d.KursId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentKurs_Kurs");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentKurs)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentKurs_Student");
            });

          

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.UserName).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(50);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
