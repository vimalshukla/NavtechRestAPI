using Models.Order;
using System.Collections.Generic;

namespace Services.Order
{
    public interface IOrderService
    {
        /// <summary>
        /// Creates Order based on Order Details.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>OrderDetails Model if created else throws exception</returns>
        CreateOrderData CreateOrder(CreateOrderData orderDetails);

        /// <summary>
        /// Get Order details.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        List<OrderDetailsAPIModel> GetOrders(int pageIndex, int pageSize, string orderBy, string direction);
       
    }
}
