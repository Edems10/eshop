using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public class ProductHelper
    {


        public static IList<Product> GenerateCarousel()
        {
            IList<Product> products = new List<Product>()
            {
                new Product(){ID=0,Nazev="temp",Price=100},
                new Product(){ID=1,Nazev="temp1",Price=100},
                new Product(){ID=2,Nazev="temp2",Price=100},
                new Product(){ID=3,Nazev="temp3",Price=100},
                new Product(){ID=4,Nazev="temp4",Price=100},

            };
            return products;
        }
    }
}
