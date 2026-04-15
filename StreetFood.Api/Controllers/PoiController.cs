using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreetFood.Api.Models;

namespace StreetFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PoiController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách địa điểm (Hỗ trợ lọc theo Role và OwnerId từ Frontend)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterest>>> GetPOIs(string? role = "Admin", string? ownerId = null)
        {
            // Nếu là Admin hoặc giá trị ownerId là 'all': Trả về tất cả các quán
            if (role == "Admin" || string.IsNullOrEmpty(ownerId) || ownerId == "all")
            {
                return await _context.PointsOfInterest.ToListAsync();
            }

            // Nếu là Chủ quán: Chỉ lọc những quán có OwnerId khớp với mã gửi lên
            return await _context.PointsOfInterest
                                 .Where(p => p.OwnerId == ownerId)
                                 .ToListAsync();
        }

        // 2. Lấy chi tiết 1 địa điểm theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PointOfInterest>> GetPOI(int id)
        {
            var poi = await _context.PointsOfInterest.FindAsync(id);
            if (poi == null) return NotFound();
            return poi;
        }

        // 3. Thêm mới địa điểm
        [HttpPost]
        public async Task<ActionResult<PointOfInterest>> PostPOI(PointOfInterest poi)
        {
            FillDefaultValues(poi);

            try
            {
                _context.PointsOfInterest.Add(poi);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPOI), new { id = poi.Id }, poi);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return BadRequest("Lỗi SQL Server: " + innerMessage);
            }
        }

        // 4. Cập nhật thông tin địa điểm
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPOI(int id, PointOfInterest poi)
        {
            if (id != poi.Id) return BadRequest("ID không khớp");

            FillDefaultValues(poi);
            _context.Entry(poi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!POIExists(id)) return NotFound();
                else throw;
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi cập nhật: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        // 5. Xóa quán ăn
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePOI(int id)
        {
            var poi = await _context.PointsOfInterest.FindAsync(id);
            if (poi == null) return NotFound();

            _context.PointsOfInterest.Remove(poi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // --- HÀM HỖ TRỢ (PRIVATE HELPER) ---

        private void FillDefaultValues(PointOfInterest poi)
        {
            if (string.IsNullOrEmpty(poi.QrCodeId))
                poi.QrCodeId = Guid.NewGuid().ToString()[..8].ToUpper();

            if (string.IsNullOrEmpty(poi.ImageUrl))
                poi.ImageUrl = "https://via.placeholder.com/150";

            if (string.IsNullOrEmpty(poi.MapLink))
                poi.MapLink = $"https://www.google.com/maps?q={poi.Latitude},{poi.Longitude}";

            if (string.IsNullOrEmpty(poi.AudioContent))
                poi.AudioContent = "Nội dung thuyết minh đang được cập nhật...";

            if (string.IsNullOrEmpty(poi.AudioFileName))
                poi.AudioFileName = "default_audio.mp3";
        }

        private bool POIExists(int id)
        {
            return _context.PointsOfInterest.Any(e => e.Id == id);
        }
    }
}