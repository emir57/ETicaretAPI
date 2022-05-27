using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ETicaretAPI.Domain.Entities
{
    public class ProductImage
    {
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductImageId { get; set; }
        public ProductImageFile ProductImageFile { get; set; }
    }
}
