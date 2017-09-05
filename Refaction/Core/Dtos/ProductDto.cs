using System;
using System.ComponentModel.DataAnnotations;

namespace Refaction.Core.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            Id = Guid.NewGuid();
            Name = String.Empty;
            Description = String.Empty;
            Price = new decimal(0d);
            DeliveryPrice = new decimal(0d);
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal DeliveryPrice { get; set; }
    }
}