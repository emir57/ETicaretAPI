using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
