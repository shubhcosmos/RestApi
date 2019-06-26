using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestProject2.Filters;
using TestProject2.Models;
using TestProject2.ActionResults;

namespace TestProject2.Controllers
{

    [RoutePrefix("returntypes")]
    [ClientCacheControlFilter(ClientCacheControl.NoCache)]
   public class ReturnTypesController : ApiController
    {
        // GET: api/ReturnTypes
        [HttpGet , Route("void")]
        public void  ReturnVoid()
        {
            
        }

        [HttpGet, Route("object")]
        public   IHttpActionResult GetObject()
        {

             var dto = new ComplexTypeDto
            {
                String1 = "this is string 1",
                String2 = "this is string 2",
                Int1 = 55,
                Int2 = 22,
                Date1 = DateTime.Now 

            };
            //try { throw new Exception("omg exception happened"); }
            //catch (Exception ex)
            //{
            //    return BadRequest();
            //}
            var res = Ok(dto);
            return res;
        }

        // GET: api/ReturnTypes/5
        [HttpPost, Route("modelstate")]
        [ValidateModelState(Bodyrequired = true)]
        public IHttpActionResult Get([FromBody] ComplexTypeDto dto)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest("int 1 should be between 1 to 100");

            return Ok(dto);
        }

        // POST: api/ReturnTypes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ReturnTypes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ReturnTypes/5
        public void Delete(int id)
        {
        }
    }
}
