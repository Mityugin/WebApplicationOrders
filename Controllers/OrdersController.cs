﻿using System;
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
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, order);
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
    }
}
