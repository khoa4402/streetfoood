using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreetFood.Api.Models; // Sửa lỗi 'UserModel' not found bằng cách trỏ đúng vào Models của Api

namespace StreetFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Đã đổi từ 'ApplicationDbContext' sang 'AppDbContext' theo file bạn vừa gửi
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            // Lấy danh sách từ bảng Users đã khai báo trong AppDbContext
            return await _context.Users.ToListAsync();
        }

        // POST: api/User - Logic tự động tăng mã OwnerId
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUser(UserModel user)
        {
            if (user.Role == "ShopOwner")
            {
                // Tìm mã OwnerId lớn nhất hiện có (ví dụ: owner_02)
                var lastOwner = await _context.Users
                    .Where(u => u.OwnerId != null && u.OwnerId.StartsWith("owner_"))
                    .OrderByDescending(u => u.OwnerId)
                    .FirstOrDefaultAsync();

                int nextNumber = 1;
                if (lastOwner != null)
                {
                    // Trích xuất số 02 từ chuỗi "owner_02"
                    string numericPart = lastOwner.OwnerId!.Replace("owner_", "");
                    if (int.TryParse(numericPart, out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }

                // Gán mã mới tăng dần: owner_03, owner_04...
                user.OwnerId = $"owner_{nextNumber:D2}";
            }
            else
            {
                user.OwnerId = null; // Admin không cần OwnerId
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }
    }
}