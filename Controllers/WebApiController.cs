using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngApp.Controllers
{
    public class WebApiController : ApiController
    {
        public string GetString(string Name)
        {
            return "Hello world Web API .... !!!" + Name;
        }
    }
}
