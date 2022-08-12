using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST_API.Models
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
}