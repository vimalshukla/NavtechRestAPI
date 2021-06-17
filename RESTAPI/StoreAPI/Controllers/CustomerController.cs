using Models.Customer;
using StoreAPI.Filter;
using Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services.Helper;

namespace StoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService service) => this.customerService = service;

        // POST api/Customer        
        [HttpPost]
        [ValidateModel]
        public HttpResponseMessage CreateCustomer([FromBody] Customer model)
        {
            try
            {

                if (customerService.CheckEmailExists(model.Email))
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, model.Email + "Email Already Exist");
                }
                model = customerService.CreateCustomer(model);
                return Request.CreateResponse(HttpStatusCode.Created, model);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        // GET api/Customer/id
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                
                Customer customer = customerService.Get(id);                
                if (customer == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, ApiConstants.CustomerNotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
