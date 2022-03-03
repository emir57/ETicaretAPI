using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Domain.Entities
{
    public class OrderProduct
    {
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
