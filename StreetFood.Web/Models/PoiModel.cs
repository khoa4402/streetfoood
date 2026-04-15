namespace StreetFood.Web.Models;

public class PoiModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Radius { get; set; }
    public int Priority { get; set; }

    // THÊM CÁC DÒNG NÀY VÀO ĐỂ HẾT LỖI ĐỎ
    public string? QrCodeId { get; set; }
    public string? ImageUrl { get; set; }
    public string? MapLink { get; set; }    
    public string? AudioContent { get; set; }
    public string? AudioFileName { get; set; }
    public string? OwnerId { get; set; }
}