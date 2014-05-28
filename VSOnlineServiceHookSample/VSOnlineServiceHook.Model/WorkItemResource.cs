using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSOnlineServiceHook.Model
{
    public class WorkItemResource
    {
        public String UpdatesUrl { get; set; }

        public IList<FieldInfo> Fields { get; set; }

        public int Id { get; set; }

        public int Rev { get; set; }

        public String Url { get; set; }

        public String WebUrl { get; set; }
    }
}
