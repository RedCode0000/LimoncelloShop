namespace LimoncelloShop.Domain.Objects
{
    public class Basket : EntityBase
    {
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public int TotalNumberOfItems { get; set; }
        public string? Cookie { get; set; }
    }
}