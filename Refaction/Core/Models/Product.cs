using System;
using System.ComponentModel.DataAnnotations;

namespace Refaction.Core.Models
{
    /// <summary>
    /// Represents the Product domain model.
    /// </summary>
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal DeliveryPrice { get; set; }
    }
}