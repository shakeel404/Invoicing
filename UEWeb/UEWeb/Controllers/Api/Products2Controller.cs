using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UEWeb.Controllers.Api
{

    [Route("api/[controller]")]
    public class Products2Controller : ApiController
    {
        // GET: api/Products2
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Products2/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products2
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products2/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products2/5
        public void Delete(int id)
        {
        }
    }
}
