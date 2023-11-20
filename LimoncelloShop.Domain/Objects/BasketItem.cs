namespace LimoncelloShop.Domain.Objects
{
    public class BasketItem : EntityBase
    {
        public int LimoncelloId { get; set; }
        public virtual Limoncello Limoncello { get; set; }
        public int Number { get; set; }
        public int BasketId { get; set; }
        public virtual Basket Basket { get; set; }
    }
}
