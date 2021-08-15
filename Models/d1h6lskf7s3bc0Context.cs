using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace backend.Models
{
    public partial class d1h6lskf7s3bc0Context : DbContext
    {
        public d1h6lskf7s3bc0Context()
        {
        }

        public d1h6lskf7s3bc0Context(DbContextOptions<d1h6lskf7s3bc0Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Benxe> Benxes { get; set; }
        public virtual DbSet<Chuyenxe> Chuyenxes { get; set; }
        public virtual DbSet<Danhgia> Danhgias { get; set; }
        public virtual DbSet<Doanhthungay> Doanhthungays { get; set; }
        public virtual DbSet<Lichsutimkiem> Lichsutimkiems { get; set; }
        public virtual DbSet<Nguoidung> Nguoidungs { get; set; }
        public virtual DbSet<Nhaxe> Nhaxes { get; set; }
        public virtual DbSet<Taikhoan> Taikhoans { get; set; }
        public virtual DbSet<Tuyenxe> Tuyenxes { get; set; }
        public virtual DbSet<Vexe> Vexes { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=ec2-3-217-68-126.compute-1.amazonaws.com;Port=5432;Database=d1h6lskf7s3bc0;User Id=ukvaniyoaldhfc;Password=3e6f937019bb71976c77e5451576cfa6def95f0968876168000e2817d354e402;sslmode=Require;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Benxe>(entity =>
            {
                entity.HasKey(e => e.MaBx)
                    .HasName("pk_benxe");

                entity.ToTable("benxe");

                entity.Property(e => e.MaBx)
                    .HasColumnName("mabx")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.TenBx)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenbx");
            });

            modelBuilder.Entity<Chuyenxe>(entity =>
            {
                entity.HasKey(e => e.MaChuyenXe)
                    .HasName("pk_chuyenxe");

                entity.ToTable("chuyenxe");

                entity.Property(e => e.MaChuyenXe)
                    .HasColumnName("machuyenxe")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DonGia).HasColumnName("dongia");

                entity.Property(e => e.GioXuatBen)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("gioxuatben");

                entity.Property(e => e.LichTrinh)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("lichtrinh");

                entity.Property(e => e.MaTuyenXe).HasColumnName("matuyenxe");

                entity.Property(e => e.MaXe).HasColumnName("maxe");

                entity.HasOne(d => d.MaTuyenXeNavigation)
                    .WithMany(p => p.Chuyenxes)
                    .HasForeignKey(d => d.MaTuyenXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_chuyenxe_matuyenxe");

                entity.HasOne(d => d.MaXeNavigation)
                    .WithMany(p => p.Chuyenxes)
                    .HasForeignKey(d => d.MaXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_chuyenxe_maxe");
            });

            modelBuilder.Entity<Danhgia>(entity =>
            {
                entity.HasKey(e => e.MaDanhGia)
                    .HasName("danhgia_pkey");

                entity.ToTable("danhgia");

                entity.HasIndex(e => new { e.MaNd, e.MaVe }, "danhgia_mand_mave_key")
                    .IsUnique();

                entity.Property(e => e.MaDanhGia)
                    .HasColumnName("madanhgia")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.MaNd).HasColumnName("mand");

                entity.Property(e => e.MaVe).HasColumnName("mave");

                entity.Property(e => e.NoiDungDanhGia)
                    .HasMaxLength(255)
                    .HasColumnName("noidungdanhgia");

                entity.Property(e => e.Sao).HasColumnName("sao");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.Danhgias)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("danhgia_mand_fkey");

                entity.HasOne(d => d.MaVeNavigation)
                    .WithMany(p => p.Danhgias)
                    .HasForeignKey(d => d.MaVe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("danhgia_mave_fkey");
            });

            modelBuilder.Entity<Doanhthungay>(entity =>
            {
                entity.HasKey(e => e.MaDoanhThuNgay)
                    .HasName("pk_doanhthungay");

                entity.ToTable("doanhthungay");

                entity.Property(e => e.MaDoanhThuNgay)
                    .HasColumnName("madoanhthungay")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(255)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Ngay)
                    .HasColumnType("date")
                    .HasColumnName("ngay");

                entity.Property(e => e.SoVe).HasColumnName("sove");

                entity.Property(e => e.TongDoanhThu).HasColumnName("tongdoanhthu");
            });

            modelBuilder.Entity<Lichsutimkiem>(entity =>
            {
                entity.HasKey(e => e.MaLichSu)
                    .HasName("pk_lichsutimkiem");

                entity.ToTable("lichsutimkiem");

                entity.Property(e => e.MaLichSu)
                    .HasColumnName("malichsu")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.MaNd).HasColumnName("mand");

                entity.Property(e => e.NgayDi)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("ngaydi");

                entity.Property(e => e.NoiDen)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("noiden");

                entity.Property(e => e.NoiDi)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("noidi");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.Lichsutimkiems)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_lichsutimkiem_mand");
            });

            modelBuilder.Entity<Nguoidung>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("pk_nhanvien");

                entity.ToTable("nguoidung");

                entity.Property(e => e.MaNd)
                    .ValueGeneratedNever()
                    .HasColumnName("mand");

                entity.Property(e => e.Cmnd)
                    .HasMaxLength(10)
                    .HasColumnName("cmnd");

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("imageurl");

                entity.Property(e => e.NgaySinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(11)
                    .HasColumnName("sdt");

                entity.Property(e => e.TenNd)
                    .HasMaxLength(50)
                    .HasColumnName("tennd");

                entity.Property(e => e.Vaitro).HasColumnName("vaitro");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithOne(p => p.Nguoidung)
                    .HasForeignKey<Nguoidung>(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_nhanvien_mand");
            });

            modelBuilder.Entity<Nhaxe>(entity =>
            {
                entity.HasKey(e => e.MaNhaXe)
                    .HasName("pk_nhaxe");

                entity.ToTable("nhaxe");

                entity.Property(e => e.MaNhaXe)
                    .HasColumnName("manhaxe")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.TenNhaXe)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("tennhaxe");
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("pk_taikhoan");

                entity.ToTable("taikhoan");

                entity.HasIndex(e => e.Email, "un_email")
                    .IsUnique();

                entity.Property(e => e.MaTk)
                    .HasColumnName("matk")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("matkhau");
            });

            modelBuilder.Entity<Tuyenxe>(entity =>
            {
                entity.HasKey(e => e.MaTuyenXe)
                    .HasName("pk_tuyenxe");

                entity.ToTable("tuyenxe");

                entity.Property(e => e.MaTuyenXe)
                    .HasColumnName("matuyenxe")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.MaBxden).HasColumnName("mabxden");

                entity.Property(e => e.MaBxdi).HasColumnName("mabxdi");

                entity.Property(e => e.MaNhaXe).HasColumnName("manhaxe");

                entity.Property(e => e.ThoiGianDiChuyen).HasColumnName("thoigiandichuyen");

                entity.HasOne(d => d.MaBxdenNavigation)
                    .WithMany(p => p.TuyenxeMaBxdenNavigations)
                    .HasForeignKey(d => d.MaBxden)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tuyenxe_mabxden");

                entity.HasOne(d => d.MaBxdiNavigation)
                    .WithMany(p => p.TuyenxeMaBxdiNavigations)
                    .HasForeignKey(d => d.MaBxdi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tuyenxe_mabxdi");

                entity.HasOne(d => d.MaNhaXeNavigation)
                    .WithMany(p => p.Tuyenxes)
                    .HasForeignKey(d => d.MaNhaXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tuyenxe_manhaxe");
            });

            modelBuilder.Entity<Vexe>(entity =>
            {
                entity.HasKey(e => e.MaVe)
                    .HasName("vexe_pkey");

                entity.ToTable("vexe");

                entity.Property(e => e.MaVe)
                    .HasColumnName("mave")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DaThanhToan).HasColumnName("dathanhtoan");

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(255)
                    .HasColumnName("ghichu");

                entity.Property(e => e.MaChoNgoi).HasColumnName("machongoi");

                entity.Property(e => e.MaChuyenXe).HasColumnName("machuyenxe");

                entity.Property(e => e.MaKh).HasColumnName("makh");

                entity.Property(e => e.NgayDi)
                    .HasMaxLength(50)
                    .HasColumnName("ngaydi");

                entity.Property(e => e.TrangThai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("true");

                entity.HasOne(d => d.MaChuyenXeNavigation)
                    .WithMany(p => p.Vexes)
                    .HasForeignKey(d => d.MaChuyenXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vexe_machuyenxe_fkey");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Vexes)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vexe_makh_fkey");
            });

            modelBuilder.Entity<Xe>(entity =>
            {
                entity.HasKey(e => e.MaXe)
                    .HasName("pk_xe");

                entity.ToTable("xe");

                entity.Property(e => e.MaXe)
                    .HasColumnName("maxe")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BienSoXe)
                    .HasMaxLength(8)
                    .HasColumnName("biensoxe")
                    .IsFixedLength(true);

                entity.Property(e => e.MaNhaXe).HasColumnName("manhaxe");

                entity.Property(e => e.MaNv).HasColumnName("manv");

                entity.Property(e => e.SoChoNgoi).HasColumnName("sochongoi");

                entity.HasOne(d => d.MaNhaXeNavigation)
                    .WithMany(p => p.Xes)
                    .HasForeignKey(d => d.MaNhaXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_xe_manhaxe");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.Xes)
                    .HasForeignKey(d => d.MaNv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_xe_manv");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
