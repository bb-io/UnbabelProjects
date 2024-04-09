using Apps.UnbabelProjects.Constants;
using Apps.UnbabelProjects.Invocables;
using Apps.UnbabelProjects.Models.Entities;
using Apps.UnbabelProjects.Models.Request.Project;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.UnbabelProjects.DataSourceHandlers;

public class ProjectDataHandler : UnbabelProjectsInvocable, IAsyncDataSourceHandler
{
    public ProjectDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new RestRequest($"/projects/v0/customers/{CustomerId}/projects:search", Method.Post);
        var items = await Client.Paginate<ProjectEntity>(request, Creds,
            JObject.FromObject(new SearchProjectsRequest()
            {
                Name = context.SearchString
            }, JsonSerializer.Create(JsonConfig.Settings)), 50);

        return items.ToDictionary(x => x.Id, x => x.Name);
    }
}