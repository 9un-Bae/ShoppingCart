using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public class ShoppingCart
    {
        public int CartNumber { get; set; }
        public string CustomerName { get; set; }
        public List<CartItem> Items { get; set; }
    }

    public class CartItem
    {
        public int ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemQuantity { get; set; }
    }

    public class Shopping
    {
        public async static Task<List<ShoppingCart>> GetAllCarts()
        {

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigurationManager.AppSettings["ShoppingRESTURL"]);
            var s = await response.Content.ReadAsStringAsync();
            List<ShoppingCart> carts = JsonConvert.DeserializeObject<List<ShoppingCart>>(s);
            return carts;
        }

        public async static Task<ShoppingCart> GetOneCart(int cartNumber)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigurationManager.AppSettings["ShoppingRESTURL"] + "/" + cartNumber.ToString());
            var s = await response.Content.ReadAsStringAsync();
            ShoppingCart cart = JsonConvert.DeserializeObject<ShoppingCart>(s);
            return cart;
        }

        public async static Task AddCart(ShoppingCart cart)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            var content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(cart),
                                Encoding.UTF8,
                                "application/json");//CONTENT-TYPE header);

            var response = await client.PutAsync(ConfigurationManager.AppSettings["ShoppingRESTURL"], content);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        public async static Task DeleteCart(int cartNumber)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(ConfigurationManager.AppSettings["ShoppingRESTURL"] + "/" + cartNumber.ToString());
        }

        public async static Task UpdateCart(ShoppingCart cart)
        {   
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            var content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(cart),
                                Encoding.UTF8,
                                "application/json");//CONTENT-TYPE header);

            var response = await client.PostAsync(ConfigurationManager.AppSettings["ShoppingRESTURL"], content);
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
