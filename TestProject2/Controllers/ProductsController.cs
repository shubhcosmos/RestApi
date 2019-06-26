using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using TestProject2.Handlers;
using TestProject2.Models;


//using Newtonsoft.Json.Converters;

namespace TestProject2.Controllers
{
    [JsonConverter(typeof(StringEnumConverter))]
   
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        List<string> lst;
        public ProductsController()
        {
             lst = new List<string> { "a", "b", "c" };
        }
       

       
        public enum Widgets
        {

            Bolt, Screw, Nut, Motor
        };
        // GET: api/Products
        [HttpGet,Route("")]
        public IEnumerable<string> shubh()
        {
           

            return new string[] { "product1", "product2 " ,Request.GetApiKey()};
        }

        // GET: api/Products/5
        [HttpGet, Route("{id:int:range(100,200)}" , Name = "GetById")]
        public int Get(int id)
        {
       
            return id;
        }
        [HttpGet ,Route("widget/{widget:enum(TestProject2.Controllers.ProductsController+Widgets)}")]
        public string GetProductwithWidgets(string widget) {

            return "Widget " + widget;

        }


        [HttpGet, Route("accounts/{accountId:validAccount}")]
        [Route("~/prods/{accountId}")]
        public string GetAccount(string accountId)
        {

            return "Widget " + accountId;



        }
        [HttpGet, Route("accounts/{accountId:validAccount}/{prodid:validAccount}")]
       // [Route("~/prods/{accountId}")]
        public string GetAccount1(string accountId, string prodid)
        {

            return "AccountID " + accountId + "  ProdiD  " + prodid;



        }

        [HttpGet, Route("complex")]
       
        public IHttpActionResult RetirnComplex([FromUri] ComplexTypeDto Dto)
        {

            return Ok(Dto);



        }

        [HttpGet, Route("segments/{*array:maxlength(256)}")] //Custom model Binder

        public IHttpActionResult RetirnSegments([ModelBinder(typeof(StringArrayWildcardBinder))] string[] array)
        {

            return Ok(array);



        }

        [HttpGet, Route("Datetime/{*datetime:datetime}")]//wild card *

        public string Returndatetime(DateTime datetime)
        {

            return datetime.ToString();



        }

        [HttpGet, Route("ReturnReferenceUrl")] //wild card *

        public string[] ReturnReferenceUrl()
        {
            var getByIdUrl = Url.Link("GetById", new { id = 123 });
            return (new string[] {
              Request.GetSelfReferenceBaseUrl().ToString(),
              Request.RebaseUrlForClient(new Uri(getByIdUrl)).ToString()
            });



        }




        // POST: api/Products
        //[HttpPost,Route("GeyById")]
        //public void Post([FromBody]string value)
        //{
        //    int id = 2;//new created after insert into db , get byid has one parameter so one prop new { Id= id})
        //    string uri = Url.Link("GeyById", new { Id = id } );
        //}

        // PUT: api/Products/5
        [HttpPost, Route("{id:int:range(100,200)}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {


        }
    }
}
