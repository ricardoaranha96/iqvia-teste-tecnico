using TesteTecnico.Models;

namespace TesteTecnico.Data
{
    public class DbInitializer
    {
        public static void Initialize(EcommerceContext context)
        {
            //context.Database.EnsureCreated();
            DbInitializer.seedProducts(context);
        }

        private static void seedProducts(EcommerceContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            var loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum in feugiat nibh, sed pulvinar mi. Morbi eleifend tempor luctus. Pellentesque eu lorem et nisl rhoncus volutpat eu non metus. Vestibulum arcu arcu, dapibus eget accumsan eget, imperdiet vel dui. Nam sit amet augue ut sem ullamcorper convallis et eget urna. Suspendisse potenti. Nam suscipit nunc ac ipsum cursus porta. Duis non enim in diam elementum dignissim. Maecenas pretium, nibh vulputate rhoncus varius, diam arcu tincidunt arcu, vitae consectetur sem odio eget nunc. Nulla vel eleifend sem, et molestie mi. Nam feugiat a turpis gravida interdum. Vivamus orci erat, sagittis at convallis eu, elementum id lorem. Phasellus rutrum tristique orci at ultrices. Phasellus placerat hendrerit magna eget dignissim. Proin sapien orci, posuere vitae fermentum sit amet, pretium ut nisi.";

            var products = new Product[]
            {
                new Product {Name="Coenzima",Description=loremIpsum,Price=40,IsFeatured=true,ProductType=ProductTypes.REMEDY},
                new Product {Name="Bio Trimag",Description=loremIpsum,Price=60,IsFeatured=true,ProductType=ProductTypes.REMEDY},
                new Product {Name="Carverol",Description=loremIpsum,Price=80,IsFeatured=true,ProductType=ProductTypes.REMEDY},
                new Product {Name="Paracetamol",Description=loremIpsum,Price=100,IsFeatured=true,ProductType=ProductTypes.REMEDY},
                new Product {Name="Coristina D",Description=loremIpsum,Price=100,ProductType=ProductTypes.REMEDY},
                new Product {Name="Head and Shoulders",Description=loremIpsum,Price=20,ProductType=ProductTypes.COSMETIC}
            };

            foreach (Product product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
}
