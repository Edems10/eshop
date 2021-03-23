using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class CarouselViewModel
    {

        public IList<Product> Products { get; set; }
        public IList <Carousel> Carousel { get; set; }
    }
}
