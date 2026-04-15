public class PointOfInterest
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; } // Thêm dấu ?
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Radius { get; set; }
    public int Priority { get; set; }

    // Đảm bảo tất cả những dòng này đều có dấu ?
    public string? QrCodeId { get; set; }
    public string? ImageUrl { get; set; }
    public string? MapLink { get; set; }
    public string? AudioContent { get; set; }
    public string? AudioFileName { get; set; }
    public string? OwnerId { get; set; } // ID của User chủ quán
}