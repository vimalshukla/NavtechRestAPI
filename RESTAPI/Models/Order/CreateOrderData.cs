using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Models.Order
{
    public class CreateOrderData
    {
        
        [Range(1, int.MaxValue, ErrorMessage = "Invalid CustomerId")]
        public int CustomerId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Invalid decimal Number")]
        public double TotalAmount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid OrderStatusId")]
        public int OrderStatusId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid PaymentTypeId")]
        public int PaymentModeId { get; set; }

        [ListHasElements(ErrorMessage = "List must contain an element")]
        public List<Products> Products { get; set; }
    }

    public class Products
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid ProductId")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid ProductId")]
        public int Quantity { get; set; }
    }

    public class ListHasElements : ValidationAttribute
    {
        public override bool IsValid(object mylist)
        {
            var list = mylist as IList;
            if (list != null)
            {
                return list.Count >= 0;
            }
            return false;
        }
    }
}
