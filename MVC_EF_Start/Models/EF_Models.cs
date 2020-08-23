using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_EF_Start.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }

        public List<Order> Orders { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }

        public Item Item { get; set; }
        public Customer Customer { get; set; }

        public double TotalPaid { get; set; }
    }
}
