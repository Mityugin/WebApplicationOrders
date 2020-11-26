using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebApplicationOrders.Models;

namespace WebApplicationOrders.Controllers
{
    public class ClientsController : ApiController
    {
        public IEnumerable<Client> Get()
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                return dbContext.Clients.ToList();
            }
        }

        public Client Get(int Number)
        {
            using (Orders_DBContext dbContext = new Orders_DBContext())
            {
                return dbContext.Clients.FirstOrDefault(e => e.Number == Number);
            }
        }
    }
}
