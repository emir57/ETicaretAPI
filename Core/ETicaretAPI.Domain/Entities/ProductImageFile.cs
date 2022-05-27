using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Domain.Entities
{
    public class ProductImageFile : File
    {
        public ICollection<ProductImage> ImageProducts { get; set; }
    }
}
