using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using VSOnlineServiceHook.Model;

namespace VSOnlineServiceHook.Api.Controllers
{
    [RoutePrefix("ServiceHooks")]
    public class ServiceHooksController : ApiController
    {
        [HttpPost]
        [Route("WorkItemCreated")]
        public IHttpActionResult WorkItemCreated(WorkItemEventData workItemEventData)
        {
            return Ok();
        }
    }
}