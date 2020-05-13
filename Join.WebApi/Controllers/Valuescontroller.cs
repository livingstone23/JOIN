using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Join.WebApi.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<String>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<String> Get(int id)
        {
            return "value1";
        }

        //Post api/values
        [HttpPost("{id}")]
        public void Post([FromBody] string value)
        {

        }

        //Put api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        //Delete api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
