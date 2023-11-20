namespace LimoncelloShop.Domain.Objects
{
    public class Limoncello : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AlcoholPercentage { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public int Stock { get; set; }
        public string? ImageFileName { get; set; }
    }
}