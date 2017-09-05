using System;
using System.ComponentModel.DataAnnotations;

namespace Refaction.Core.Models
{
    /// <summary>
    /// Represents the ProductOption domain model.
    /// </summary>
    public class ProductOption
    {
        public Guid Id { get; set; }

        [Required] 
        [StringLength(100)]
        public string Name { get; set; }

        [Required] 
        [StringLength(500)]
        public string Description { get; set; }

        public Guid ProductId { get; set; }

        /// <summary>
        /// Navigation property for <see cref="Product"/>
        /// </summary>
        public Product Product { get; set; }
    }
}