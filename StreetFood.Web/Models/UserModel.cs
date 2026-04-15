namespace StreetFood.Web.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = ""; // 'Admin' hoặc 'ShopOwner'
        public string? OwnerId { get; set; }
        public string? FullName { get; set; }
    }
}