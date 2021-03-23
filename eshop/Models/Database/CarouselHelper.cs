using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class CarouselHelper
    {
        public static IList<Carousel> GenerateCarousel()
        {
            IList<Carousel> carousels = new List<Carousel>()
            {
                new Carousel(){DataTarget="#myCarousel",ImageSrc="/images/banner1.svg",ImageAlt="ASP>NET",CarouselContent="Learn how to build ASP.NET apps that can run anywhere." + " <a class=\"btn btn-default\" href=\"https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409\">" + "Learn More </a>"},
                new Carousel(){DataTarget="#myCarousel",ImageSrc="/images/banner2.svg",ImageAlt="ASP>NET",CarouselContent="There are powerful new features in Visual Studio for building modern web apps.  <a class=\"btn btn-default\" href=\"https://go.microsoft.com/fwlink/?LinkID=525030&clcid=0x409\">Learn More</a>"},
                new Carousel(){DataTarget="#myCarousel",ImageSrc="/images/banner3.svg",ImageAlt="ASP>NET",CarouselContent="Learn how Microsoft's Azure cloud platform allows you to build, deploy, and scale web apps."+"<a class=\"btn btn-default\" href=\"https://go.microsoft.com/fwlink/?LinkID=525027&clcid=0x409\"> Learn More </a>"},
                new Carousel(){DataTarget="#myCarousel",ImageSrc="/images/carousel/peepo.png",ImageAlt="INFORMATION",CarouselContent="Learn how to build ASP.NET apps that can run anywhere." + " <a class=\"btn btn-default\" href=\"https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409\">" + "Learn More </a>"},
            };
            return carousels;
        }
    }
}
