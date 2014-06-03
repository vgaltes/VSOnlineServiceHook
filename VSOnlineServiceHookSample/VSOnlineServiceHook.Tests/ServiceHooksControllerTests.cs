using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VSOnlineServiceHook.Model;
using FluentAssertions;
using System.Net.Http.Formatting;
using System.Collections.Generic;

namespace VSOnlineServiceHook.Tests
{
    [TestClass]
    public class ServiceHooksControllerTests
    {
        [TestMethod]
        //http://www.visualstudio.com/integrate/get-started/get-started-service-hooks-events-vsi#workitemiscreatedworkitem.created
        public async Task WhenReceivingAWorkItemCreated_ShouldAddAnEventToDatabase()
        {
            DBHelper.ClearWorkItemsTable();

            using ( var server = TestServer.Create<TestStartup>())
            {
                var workItem = new WorkItemEventData()
                {
                    EventType = "workitem.created",
                    NotificationId = 1,
                    Resource = new WorkItemResource()
                    {
                        Fields = new List<FieldInfo>
                        {
                            new FieldInfo{
                                Field = new FieldDetailedInfo
                                {
                                    Id = 1,
                                    Name = "Title",
                                    RefName = "System.Title"
                                },
                                Value = "New work item"
                            }
                        }
                    },
                    SubscriptionId = "11c6b191-6b61-4ca0-96ed-99719ecdd916"
                };

                HttpResponseMessage response = await server.HttpClient.PostAsync("ServiceHooks/WorkItemCreated",
                    workItem, new JsonMediaTypeFormatter());

                response.IsSuccessStatusCode.Should().BeTrue();

                // Check database
                var workItems = DBHelper.GetWorkItems();
                workItems.Should().HaveCount(1);
                workItems[0].Should().Be("New work item");
            }
        }
    }
}