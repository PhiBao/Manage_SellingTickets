using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace backend.Models
{
    public partial class QLBVXKContext : DbContext
    {
        public QLBVXKContext()
        {
        }

        public QLBVXKContext(DbContextOptions<QLBVXKContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Benxe> Benxes { get; set; }
        public virtual DbSet<Chongoi> Chongois { get; set; }
        public virtual DbSet<Chuyenxe> Chuyenxes { get; set; }
        public virtual DbSet<Doanhthungay> Doanhthungays { get; set; }
        public virtual DbSet<Nguoidung> Nguoidungs { get; set; }
        public virtual DbSet<Taikhoan> Taikhoans { get; set; }
        public virtual DbSet<Tuyenxe> Tuyenxes { get; set; }
        public virtual DbSet<Vexe> Vexes { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=mssql-29154-0.cloudclusters.net, 29184;Initial Catalog=qlbvxk;User Id=kid;Password=myPass123;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Benxe>(entity =>
            {
                entity.HasKey(e => e.MaBx);

                entity.ToTable("BENXE");

                entity.Property(e => e.MaBx).HasColumnName("MaBX");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TenBx)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TenBX");
            });

            modelBuilder.Entity<Chongoi>(entity =>
            {
                entity.HasKey(e => e.MaChoNgoi);

                entity.ToTable("CHONGOI");

                entity.HasOne(d => d.MaChuyenXeNavigation)
                    .WithMany(p => p.Chongois)
                    .HasForeignKey(d => d.MaChuyenXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHONGOI_MaChuyenXe");
            });

            modelBuilder.Entity<Chuyenxe>(entity =>
            {
                entity.HasKey(e => e.MaChuyenXe);

                entity.ToTable("CHUYENXE");

                entity.Property(e => e.NgayXuatBen).HasColumnType("datetime");

                entity.HasOne(d => d.MaTuyenXeNavigation)
                    .WithMany(p => p.Chuyenxes)
                    .HasForeignKey(d => d.MaTuyenXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHUYENXE_MaTuyenXe");

                entity.HasOne(d => d.MaXeNavigation)
                    .WithMany(p => p.Chuyenxes)
                    .HasForeignKey(d => d.MaXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHUYENXE_MaXe");
            });

            modelBuilder.Entity<Doanhthungay>(entity =>
            {
                entity.HasKey(e => e.MaDoanhThuNgay);

                entity.ToTable("DOANHTHUNGAY");

                entity.Property(e => e.Ngay).HasColumnType("date");
            });

            modelBuilder.Entity<Nguoidung>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK_NHANVIEN");

                entity.ToTable("NGUOIDUNG");

                entity.Property(e => e.MaNd)
                    .ValueGeneratedNever()
                    .HasColumnName("MaND");

                entity.Property(e => e.Cmnd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CMND");

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNd)
                    .HasMaxLength(50)
                    .HasColumnName("TenND");

                entity.Property(e => e.Vaitro).HasColumnName("VAITRO");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithOne(p => p.Nguoidung)
                    .HasForeignKey<Nguoidung>(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NHANVIEN_MaND");
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.MaTk);

                entity.ToTable("TAIKHOAN");

                entity.HasIndex(e => e.Email, "UN_EMAIL")
                    .IsUnique();

                entity.Property(e => e.MaTk).HasColumnName("MaTK");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tuyenxe>(entity =>
            {
                entity.HasKey(e => e.MaTuyenXe);

                entity.ToTable("TUYENXE");

                entity.Property(e => e.MaBxden).HasColumnName("MaBXDen");

                entity.Property(e => e.MaBxdi).HasColumnName("MaBXDi");

                entity.HasOne(d => d.MaBxdenNavigation)
                    .WithMany(p => p.TuyenxeMaBxdenNavigations)
                    .HasForeignKey(d => d.MaBxden)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TUYENXE_MaBXDen");

                entity.HasOne(d => d.MaBxdiNavigation)
                    .WithMany(p => p.TuyenxeMaBxdiNavigations)
                    .HasForeignKey(d => d.MaBxdi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TUYENXE_MaBXDi");
            });

            modelBuilder.Entity<Vexe>(entity =>
            {
                entity.HasKey(e => e.MaVe);

                entity.ToTable("VEXE");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.HasOne(d => d.MaChoNgoiNavigation)
                    .WithMany(p => p.Vexes)
                    .HasForeignKey(d => d.MaChoNgoi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VeXe_MaChoNgoi");

                entity.HasOne(d => d.MaChuyenXeNavigation)
                    .WithMany(p => p.Vexes)
                    .HasForeignKey(d => d.MaChuyenXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VeXe_MaChuyenXe");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Vexes)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VeXe_MaKH");
            });

            modelBuilder.Entity<Xe>(entity =>
            {
                entity.HasKey(e => e.MaXe);

                entity.ToTable("XE");

                entity.Property(e => e.BienSoXe)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.NhaXe).HasMaxLength(100);

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.Xes)
                    .HasForeignKey(d => d.MaNv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_XE_MaNV");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
