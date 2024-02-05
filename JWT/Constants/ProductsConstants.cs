using JWT.Models;

namespace JWT.Constants
{
    public class ProductsConstants
    {
        public static List<ProductsModel> Products = new List<ProductsModel>()
        {
            new ProductsModel{Name="Laptop",Description="Computadora"},
            new ProductsModel{Name="Moto",Description= "Vehiculo" }
        };
    }
}
