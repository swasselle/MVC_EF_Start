using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.DataAccess;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.Controllers
{
  public class DatabaseExampleController : Controller
  {
    public ApplicationDbContext dbContext;

    public DatabaseExampleController(ApplicationDbContext context)
    {
      dbContext = context;
    }

    public IActionResult Index()
    {
            Customer customer1 = new Customer();
            customer1.Name = "Sarah Wasselle";

            Customer customer2 = new Customer();
            customer2.Name = "Lorinska B";

            Item item1 = new Item();
            item1.ItemName = "Dog Food";

            Item item2 = new Item();
            item2.ItemName = "Dog Bowls";

            Item item3 = new Item();
            item3.ItemName = "Blanket";

            Item item4 = new Item();
            item4.ItemName = "Towels";

            Order order1 = new Order();
            order1.Customer = customer1;
            order1.Item = item1;
            order1.TotalPaid = 6.39;

            Order order2 = new Order();
            order2.Customer = customer1;
            order2.Item = item2;
            order2.TotalPaid = 11.75;

            Order order3 = new Order();
            order3.Customer = customer2;
            order3.Item = item3;
            order3.TotalPaid = 19.27;

            Order order4 = new Order();
            order4.Customer = customer2;
            order4.Item = item4;
            order4.TotalPaid = 13.65;

            dbContext.Customers.Add(customer1);
            dbContext.Customers.Add(customer2);

            dbContext.Items.Add(item1);
            dbContext.Items.Add(item2);
            dbContext.Items.Add(item3);
            dbContext.Items.Add(item4);

            dbContext.Orders.Add(order1);
            dbContext.Orders.Add(order2);
            dbContext.Orders.Add(order3);
            dbContext.Orders.Add(order4);

            dbContext.SaveChanges();
            return View();
    }

        public IActionResult DatabaseOperations()
        {
            // Query 1. Aggregations
            int orderCount = dbContext.Orders.Count();
            int customerCount = dbContext.Customers.Count();

            ViewData["order_count"] = orderCount;
            ViewData["customer_count"] = customerCount;

            // Query 2: Read all customers
            // Features include which includes Orders associated with each Customer
            Customer[] CustomerRead1 = dbContext.Customers
                                  .Include(c => c.Orders)
                                  .ThenInclude(o => o.Item)
                                  .Take(100)
                                  .ToArray();

            return View(CustomerRead1);
        }


        public IActionResult PointQueryAction(int id)
        {

            // Query 3: Customer point query based on id in url
            Customer CustomerDetailRead = dbContext.Customers
                                    .Include(c => c.Orders)
                                    .ThenInclude(o => o.Item)
                                    .Where(c => c.Id == id)
                                    .First();

            return View(CustomerDetailRead);
        }
    }
}