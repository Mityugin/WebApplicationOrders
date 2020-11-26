using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebApplicationOrders.Models;

namespace WebApplicationOrders.Controllers
{
    public class GoodsController : ApiController
    {
        public IEnumerable<Good> Get()
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                return dbContext.Goods.ToList();
            }
        }

        public Good Get(int Number)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                return dbContext.Goods.FirstOrDefault(e => e.Number == Number);
            }
        }
    }
}
