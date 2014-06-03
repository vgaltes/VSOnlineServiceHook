using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSOnlineServiceHook.Model;

namespace VSOnlineServiceHook.Data
{
    public class EventsRepository
    {
        EventsContext eventsContext;

        public EventsRepository()
        {
            this.eventsContext = new EventsContext();
        }

        public void AddEvent(VSOnlineEvent newEvent)
        {
            eventsContext.Events.Add(newEvent);
            eventsContext.SaveChanges();
        }
    }
}