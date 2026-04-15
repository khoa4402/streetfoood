using Microsoft.EntityFrameworkCore;
using StreetFood.Api.Models; // Đảm bảo dòng này có để nhận diện lớp UserModel

namespace StreetFood.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Quản lý bảng PointsOfInterest (Dữ liệu quán ăn)
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        // THÊM DÒNG NÀY: Để quản lý bảng Users (Tài khoản và Phân quyền)
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình thêm cho bảng Users (nếu cần)
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("Users"); // Đảm bảo ánh xạ đúng tên bảng bạn đã tạo trong SQL
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Username).IsUnique(); // Ràng buộc Unique cho tài khoản
            });
        }
    }
}