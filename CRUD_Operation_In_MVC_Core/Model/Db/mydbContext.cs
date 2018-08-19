using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUD_Operation_In_MVC_Core.Model.Db
{
    public partial class mydbContext : DbContext
    {
        public virtual DbSet<Examresult> Examresult { get; set; }
        public virtual DbSet<Fee> Fee { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Mystuinfo> Mystuinfo { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
        // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=mydb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Examresult>(entity =>
            {
                entity.HasKey(e => new { e.Examsl, e.Studentid });

                entity.ToTable("examresult");

                entity.Property(e => e.Examsl).HasColumnName("examsl");

                entity.Property(e => e.Studentid).HasColumnName("studentid");

                entity.Property(e => e.Board)
                    .HasColumnName("board")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Examname)
                    .HasColumnName("examname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Institution)
                    .HasColumnName("institution")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Result)
                    .HasColumnName("result")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Examresult)
                    .HasForeignKey(d => d.Studentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__examresul__stude__3D5E1FD2");
            });

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.HasKey(e => e.Voucherno);

                entity.Property(e => e.Voucherno)
                    .HasColumnName("voucherno")
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.Headname)
                    .HasColumnName("headname")
                    .HasMaxLength(100);

                entity.Property(e => e.InputDate)
                    .HasColumnName("inputDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.FeeNavigation)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Fee__studentId__3A81B327");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.Itemcode);

                entity.ToTable("items");

                entity.Property(e => e.Itemcode)
                    .HasColumnName("itemcode")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Itemname)
                    .HasColumnName("itemname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mystuinfo>(entity =>
            {
                entity.ToTable("mystuinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fee)
                    .HasColumnName("fee")
                    .HasColumnType("money");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasMaxLength(500);
            });
        }
    }
}
