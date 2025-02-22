
namespace WatchLk.CartService.Domains.Model
{
    public class CartItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string UserId { get; set; }
        public required string Brand { get; set; }
        public required string Category { get; set; }
        public required string ImageUrl { get; set; }
        public double UnitPrice { get; set; }
        public int Quantiy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
