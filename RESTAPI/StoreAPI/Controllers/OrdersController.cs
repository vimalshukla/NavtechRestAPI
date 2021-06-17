using Models.Order;
using Services.Helper;
using Services.Order;
using StoreAPI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderService service;

        public OrdersController(IOrderService service) => this.service = service; //Dependency Injection

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ValidateModel]
        public HttpResponseMessage Order([FromBody] CreateOrderData orderDetails)
        {
            try
            {
                service.CreateOrder(orderDetails);
                return Request.CreateResponse(HttpStatusCode.Created, orderDetails);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }

        /// <summary>
        /// Get Orders based on Pagesize, pageIndex
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isSortedByAscending"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage Orders(int pageIndex, int pageSize, string orderBy = ApiConstants.OrderByID, string direction = ApiConstants.SortAscending)
        {
            try
            {
                if (!InvalidParameters(orderBy, direction))
                {
                    List<OrderDetailsAPIModel> orderDetails = service.GetOrders(pageIndex, pageSize, orderBy, direction);
                    if (orderDetails!=null && orderDetails.Count>0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, orderDetails);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, ApiConstants.OrdersNotFound);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ApiConstants.InvalidParameter);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        /// <summary>
        /// This Function Checke if Query paramater are allowed
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool InvalidParameters(string orderBy, string direction)
        {
            bool isInvalidParameters = true;
            if (ApiConstants.AllowedOrderByList.Contains(orderBy, StringComparer.OrdinalIgnoreCase)&&
                ApiConstants.AllowedSortDirections.Contains(direction, StringComparer.OrdinalIgnoreCase))
            {
                isInvalidParameters = false;
            }
            return isInvalidParameters;
        }
    }
}