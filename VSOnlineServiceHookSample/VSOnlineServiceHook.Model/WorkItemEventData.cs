using System;
namespace VSOnlineServiceHook.Model
{
    // http://www.visualstudio.com/integrate/get-started-service-hooks-creating-and-managing-vsi
    public class WorkItemEventData
    {
        public String SubscriptionId { get; set; }

        public int NotificationId { get; set; }

        public String EventType { get; set; }

        public WorkItemResource Resource { get; set; }
    }
}