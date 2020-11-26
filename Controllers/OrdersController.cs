using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebApplicationOrders.Models;

namespace WebApplicationOrders.Controllers
{
    public class OrdersController : ApiController
    {
        public IEnumerable<Order> Get()
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                return dbContext.Orders.ToList();
            }
        }

        public Order Get(int Number)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                return dbContext.Orders.FirstOrDefault(e => e.Number == Number);
            }
        }

        // POST: api/Orders
        public HttpResponseMessage Post([FromBody] Order order)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, order);
            }
        }

        // PUT: api/Orders/5
        public HttpResponseMessage Put(int Number, [FromBody] Order order)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                var order1 = dbContext.Orders.Find(Number);
                order1.Description = order.Description;
                order1.TotalPrice = order.TotalPrice;

                dbContext.Entry(order1).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
