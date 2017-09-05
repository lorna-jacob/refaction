using System;
using System.ComponentModel.DataAnnotations;

namespace Refaction.Core.Dtos
{
    public class ProductOptionDto
    {
        public ProductOptionDto()
        {
            Id = Guid.NewGuid();
            Name = String.Empty;
            Description = String.Empty;
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}