using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UEWeb.Context;
using UEWeb.Models;

namespace UEWeb.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private UEDbContext Db = new UEDbContext();

        public IEnumerable<Product> Get()
        {
            return Db.Products.Take(5);
        }
        public IEnumerable<Product> Get(string query)
        {
            return Db.Products.Where(p => p.Name.Contains(query) || p.Code.Contains(query)).AsEnumerable();
        }

         
        public async Task<Product> Get(int id)
        {
            return await Db.Products.FindAsync(id);
        }

         
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        
        public void Delete(int id)
        {
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
