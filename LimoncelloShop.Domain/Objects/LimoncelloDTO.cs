namespace LimoncelloShop.Domain.Objects
{
    public class LimoncelloDTO
    {
        public static LimoncelloDTO ToLimoncelloDTO(Limoncello lemoncello)
        {
            return new LimoncelloDTO
            {
                Name = lemoncello.Name,
                Description = lemoncello.Description,
                AlcoholPercentage = lemoncello.AlcoholPercentage,
                Price = lemoncello.Price,
                Volume = lemoncello.Volume,
                Stock = lemoncello.Stock,
                ImageFileName = lemoncello.ImageFileName
            };
        }

        public static Limoncello ToLimoncello(LimoncelloDTO model)
        {
            return new Limoncello
            {
                Name = model.Name,
                Description = model.Description,
                AlcoholPercentage = model.AlcoholPercentage,
                Price = model.Price,
                Volume = model.Volume,
                Stock = model.Stock,
                ImageFileName = model.ImageFileName
            };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AlcoholPercentage { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public int Stock { get; set; }
        public string? ImageFileName { get; set; }
    }
}
