using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public class ApiConstants
    {
        #region Stored Procedures
        public const string P_GetOrderList = "P_GetOrderList";
        public const string P_CreateOrderWithProductDetails = "P_CreateOrderWithProductDetails";
        
        public const string SortAscending = "asc";
        public const string SortDescending = "desc";
        #endregion

        #region Query Param Constraints
        public const string OrderByID = "Id";
        public const string OrderByStatus = "Status";
        public static readonly string[] AllowedOrderByList  = new string[]{ OrderByID, OrderByStatus };
        public static readonly string[] AllowedSortDirections = new string[] { SortAscending, SortDescending };
        #endregion

        #region Validation Message
        public const string OrdersNotFound = "No orders found for the given input.";
        public const string CustomerNotFound = "No Customer found for the given input.";
        public const string InvalidPageInput = "Invalid Page size or Page index.";
        public const string OrderCreatedSuccessfully = "Order created successfully.";
        public const string InvalidParameter = "Invalid Parameter or Value please refer API Document.";

        #endregion


    }
}
