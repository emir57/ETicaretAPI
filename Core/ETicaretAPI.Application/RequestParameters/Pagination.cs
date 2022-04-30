using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.RequestParameters
{
    public struct Pagination
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
