using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Linq;

namespace REST_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = true;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            //GlobalConfiguration.Configuration.EnableSwagger(c => c.SingleApiVersion("v1", "Australian Towns API"))
            //            .EnableSwaggerUi();

            List<Models.ShoppingCart> db = new List<Models.ShoppingCart>()
            {
                new Models.ShoppingCart()
                {
                    CartNumber =111,
                    CustomerName ="TestCustomer",
                    Items = new List<Models.CartItem>()
                    {
                        new Models.CartItem
                        {
                            ItemNumber=1,
                            ItemDescription="Dummy ItemA",
                            ItemPrice=22,
                            ItemQuantity=66

                        },
                        new Models.CartItem
                        {
                            ItemNumber=2,
                            ItemDescription="Dummy ItemB",
                            ItemPrice=77,
                            ItemQuantity=99

                        }
                    }
                },
                 new Models.ShoppingCart()
                {
                    CartNumber =222,
                    CustomerName ="TestCustomer2",
                    Items = new List<Models.CartItem>()
                    {
                        new Models.CartItem
                        {
                            ItemNumber=1,
                            ItemDescription="Dummy ItemA2",
                            ItemPrice=22,
                            ItemQuantity=66

                        },
                        new Models.CartItem
                        {
                            ItemNumber=2,
                            ItemDescription="Dummy ItemB2",
                            ItemPrice=77,
                            ItemQuantity=99

                        }
                    }
                }
            };


            Application.Add("inMemoryDatabase", db);
        }                   

    }
}
