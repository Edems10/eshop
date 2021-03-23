using eshop.Models.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    [Table("Product")]public class Product
    {
        [Key]
        [Required]
        [Column("ID")]
        public int ID { get; set; }
        [Required]
        [Column("Name")]
        public string Nazev { get; set; }
        [Required]
        [Column("Category")]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }
        [NotMapped]
        [FileContentType("image")]
        public IFormFile Image { get; set; }
        [Required]
        [StringLength(255)]
        public string ImageSrc { get; set; }
        [Required]
        [StringLength(25)]
        public string ImageAlt { get; set; }
        public string DetailProduktu { get; set; }
    }
}
