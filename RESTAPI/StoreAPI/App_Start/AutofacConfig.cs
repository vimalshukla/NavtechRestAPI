using Autofac;
using Autofac.Integration.WebApi;
using Services.Customers;
using Services.Order;
using System;
using System.Reflection;
using System.Web.Http;

namespace StoreAPI.App_Start
{
    public class AutofacWebAPIConfig
    {
        public static void Initialize(HttpConfiguration config)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(
                RegisterServices(new ContainerBuilder())
            );
        }
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            try
            {
                builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();
                builder.RegisterType<CustomerService>().As<ICustomerService>();
                builder.RegisterType<OrderService>().As<IOrderService>();

                return builder.Build();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}