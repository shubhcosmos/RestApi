using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestProject2.Authorization;

namespace TestProject2.Controllers
{



    [RoutePrefix("api/auth")]
    
    public class AuthController : ApiController
    {
        // GET: api/Auth
       
        [HttpGet ,Route("")]
       //[Authorize(Users = "TestUserNam")]
      //[OverrideAuthorization]
      //[AllowAnonymous]
       // [RequireHttpsAttribute]
      // [RequiresClaimAttribute("MyCustomClaim" , IncludeMissingInresponse=true)]


        public IEnumerable<string> Get()
        {
            // return new string[] { User.Identity.Name , User.Identity.AuthenticationType };

            throw new ArgumentNullException();
        }

        // GET: api/Auth/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Auth
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Auth/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Auth/5
        public void Delete(int id)
        {
        }
    }
}
