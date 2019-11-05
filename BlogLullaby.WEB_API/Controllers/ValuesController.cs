using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLullaby.BLL.UserCommunicatingService;
using BlogLullaby.WEB_API.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlogLullaby.WEB_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IHostingEnvironment _appEnvironment;
        private AppConfig _appConfig;
        public ValuesController(IHostingEnvironment appEnvironment, IOptions<AppConfig> appConfig)
        {
            _appEnvironment = appEnvironment;
            _appConfig = appConfig.Value;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return $"{_appEnvironment.WebRootPath}";
        }

        [HttpGet("{id}")]
        public ActionResult<int> Get(int id)
        {
            return (id / 0);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
