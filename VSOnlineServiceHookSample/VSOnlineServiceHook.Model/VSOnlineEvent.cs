using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSOnlineServiceHook.Model
{
    public class VSOnlineEvent
    {
        public VSOnlineEvent(string title)
        {
            Title = title;
        }

        public int EventId { get; set; }
        public string Title { get; set; }
    }
}