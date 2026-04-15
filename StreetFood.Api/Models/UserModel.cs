namespace StreetFood.Api.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = ""; // 'Admin' hoặc 'ShopOwner'
        public string? OwnerId { get; set; }  // 'owner_01', 'owner_02'...
        public string? FullName { get; set; }
    }
}