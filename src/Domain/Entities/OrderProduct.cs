using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OrderProduct
    {
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
