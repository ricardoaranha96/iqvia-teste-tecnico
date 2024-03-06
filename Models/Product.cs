namespace TesteTecnico.Models
{
    public enum ProductTypes
    {
        REMEDY,
        COSMETIC
    }

    public class Product
    {
        public int Id { get; set; }

        public ProductTypes ProductType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Boolean IsFeatured { get; set; } = false;

        public String getProductTypeName()
        {
            switch (ProductType)
            {
                case ProductTypes.REMEDY:
                    return "Remédio";
                case ProductTypes.COSMETIC:
                    return "Cosmético";
                default:
                    throw new Exception("Tipo de produto inválido");
            }
        }
    }
}
