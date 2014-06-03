using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using VSOnlineServiceHook.Model;
using VSOnlineServiceHook.Data;

namespace VSOnlineServiceHook.Api.Controllers
{
    [RoutePrefix("ServiceHooks")]
    public class ServiceHooksController : ApiController
    {
        [HttpPost]
        [Route("WorkItemCreated")]
        public IHttpActionResult WorkItemCreated(WorkItemEventData workItemEventData)
        {
            var workItemTitle = workItemEventData.Resource.Fields
                .First(f => f.Field.RefName == "System.Title").Value;
            
            var eventsRepository = new EventsRepository();
            eventsRepository.AddEvent(new VSOnlineEvent(workItemTitle));

            return Ok();
        }
    }
}