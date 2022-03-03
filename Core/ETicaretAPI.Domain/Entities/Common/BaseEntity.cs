using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
