using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestProject2.Filters;

namespace TestProject2.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET: api/Values
        [HttpGet,Route("")]
       // [RouteTimerFilter("GetAllValues")]
        [ClientCacheControlFilter(ClientCacheControl.Public ,10 )]


        public  IHttpActionResult Get()
        {
           // Trace.WriteLine( + DateTime.Now);
             return Ok(new string[]{ "value1", "value2 "  , DateTime.Now.ToString()});
        }

        // GET: api/Values/5
        [HttpGet, Route("{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        [HttpPost,Route("")]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}
