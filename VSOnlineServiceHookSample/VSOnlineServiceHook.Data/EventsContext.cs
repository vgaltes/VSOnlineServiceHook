using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSOnlineServiceHook.Model;

namespace VSOnlineServiceHook.Data
{
    public class EventsContext : DbContext
    {
        public EventsContext() : base("ServiceHooksEvents") { }

        public DbSet<VSOnlineEvent> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VSOnlineEvent>().ToTable("Events");
            modelBuilder.Entity<VSOnlineEvent>().HasKey(e => e.EventId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
