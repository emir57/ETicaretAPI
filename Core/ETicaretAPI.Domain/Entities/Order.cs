using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            //Products = new List<Product>();
        }
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public Customer Customer { get; set; }
    }
}
