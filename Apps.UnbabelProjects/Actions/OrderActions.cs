using Apps.UnbabelProjects.Constants;
using Apps.UnbabelProjects.Invocables;
using Apps.UnbabelProjects.Models.Entities;
using Apps.UnbabelProjects.Models.Request.Order;
using Apps.UnbabelProjects.Models.Request.Project;
using Apps.UnbabelProjects.Models.Response.Order;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.UnbabelProjects.Actions;

[ActionList]
public class OrderActions : UnbabelProjectsInvocable
{
    public OrderActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List orders", Description = "List all orders")]
    public async Task<ListOrdersResponse> ListOrders([ActionParameter] ProjectRequest input)
    {
        var request = new RestRequest($"/projects/v0/customers/{CustomerId}/projects/{input.ProjectId}/orders");
        var items = await Client.Paginate<OrderEntity>(request, Creds);

        return new(items);
    }

    [Action("Search orders", Description = "Search for orders based on the provided criterias")]
    public async Task<ListOrdersResponse> SearchOrders(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] SearchOrdersRequest input)
    {
        var request = new RestRequest($"/projects/v0/customers/{CustomerId}/projects/{project.ProjectId}/orders");
        var items = await Client.Paginate<OrderEntity>(request, Creds,
            JObject.FromObject(input, JsonSerializer.Create(JsonConfig.Settings)));

        return new(items);
    }
}