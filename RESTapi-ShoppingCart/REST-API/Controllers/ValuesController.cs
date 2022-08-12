using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using N=Newtonsoft.Json;

namespace REST_API.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public List<Models.ShoppingCart> Get()
        {
            return HttpContext.Current.Application[0] as List<Models.ShoppingCart>;
        }

        //// GET api/values/1111
        [HttpGet]
        public Models.ShoppingCart Get(int cartNumber)
        {
            List<Models.ShoppingCart> carts = HttpContext.Current.Application[0] as List<Models.ShoppingCart>;
            return carts.Where(sc => sc.CartNumber == cartNumber).FirstOrDefault();
        }


        // PUT api/values  
        [HttpPut]
        public void Put(Models.ShoppingCart sc)
        {
            List<Models.ShoppingCart> carts = HttpContext.Current.Application[0] as List<Models.ShoppingCart>;
            carts.Add(new Models.ShoppingCart { CartNumber = sc.CartNumber, CustomerName = sc.CustomerName, Items= sc.Items });

        }

        // Patch api/values     
        [HttpPost]
        public void Patch(Models.ShoppingCart sc)
        {
            List<Models.ShoppingCart> carts = HttpContext.Current.Application[0] as List<Models.ShoppingCart>;
            if (carts.Where(x => x.CartNumber == sc.CartNumber).Count() == 1)
            {
                Models.ShoppingCart cartToUpdate = carts.Where(x => x.CartNumber == sc.CartNumber).First();
                cartToUpdate.CustomerName = sc.CustomerName;
                Models.CartItem[] items= new Models.CartItem[sc.Items.Count];
                sc.Items.CopyTo(items);
                cartToUpdate.Items = items.ToList();
            }
        }

        // DELETE api/values/1111
        [HttpDelete]
        public void Delete(int cartNumber)
        {
            List<Models.ShoppingCart> carts = HttpContext.Current.Application[0] as List<Models.ShoppingCart>;
            if (carts.Where(sc => sc.CartNumber == cartNumber).Count() == 1)
                carts.Remove(carts.Where(sc => sc.CartNumber == cartNumber).First());
        }
    }


}
