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
        // GET ORDERS
        public IEnumerable<Order> Get()
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                return dbContext.Orders.ToList();
            }
        }

        // GET ORDERS by CLIENTNUMBER
        [HttpGet]
        [Route("~/api/orders/{ClientNumber:int}")]
        public IEnumerable<Order> Get(int ClientNumber)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                //return dbContext.Orders.Where(e => e.ClientNumber == ClientNumber);

                var orders = dbContext.Orders;
                foreach (var order in orders)
                {
                    if (order.ClientNumber == ClientNumber)
                    {
                        yield return order;
                    }

                }

            }
        }

        // GET PRODUCTS in ORDER
        [HttpGet]
        [Route("~/api/orders/{Number:int}/goods")]
        public IEnumerable<Good> GetDetail(int Number)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                //return dbContext.Orders.Where(e => e.ClientNumber == ClientNumber);

                var orders = dbContext.Orders;
                foreach (var order in orders)
                {
                    if (order.Number == Number)
                    {
                        var goods = dbContext.Goods;
                        foreach (var good in goods)
                        {
                            if (order.Description.Contains(good.Number.ToString()))
                            {
                                yield return good;
                            }
                        }
                        
                    }

                }

            }
        }

        // POST: api/Orders
        public HttpResponseMessage Post([FromBody] Order order)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                Order order1 = new Order();
                order1.ClientNumber = order.ClientNumber;
                order1.Description = order.Description;
                
                order1.TotalPrice = 0;

                var goods = dbContext.Goods;
                foreach (var good in goods)
                {
                    if (order1.Description.Contains(good.Number.ToString()))
                    {
                        order1.TotalPrice += good.Price;
                    }
                }

                // Если Клиент VIP уменьшаем общую сумму заказа на размер скидки, но не более 50%
                if (ClientIsVip(order1.ClientNumber))
                {
                    var FullPrice = order1.TotalPrice;

                    var orders = dbContext.Orders;
                    foreach (var oldorder in orders)
                    {
                        if (oldorder.ClientNumber == order1.ClientNumber)
                        {
                            if (order1.TotalPrice > FullPrice / 2)
                            {
                                order1.TotalPrice -= 1;
                            }

                        }

                    }

                }

                

                dbContext.Orders.Add(order1);
                dbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, order1);
            }
        }

        // PUT: api/Orders/
        public HttpResponseMessage Put(int Number, [FromBody] Order order)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                var order1 = dbContext.Orders.Find(Number);
                order1.ClientNumber = order.ClientNumber;
                order1.Description = order.Description;
                order1.TotalPrice = order.TotalPrice;

                dbContext.Entry(order1).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        private bool ClientIsVip(int ClientNumber)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                bool IsVip = false;
                var clients = dbContext.Clients;
                foreach (var client in clients)
                {
                    if (client.Number == ClientNumber)
                    {
                        IsVip = true;
                    }

                }
                return IsVip;
            }
        }
    }
}
