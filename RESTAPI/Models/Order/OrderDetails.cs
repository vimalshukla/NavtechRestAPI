using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Order
{
    public class OrderDetailsAPIModel
    {
        public int OrderId { get; set; }
        public double TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMode { get; set; }
    }
}
