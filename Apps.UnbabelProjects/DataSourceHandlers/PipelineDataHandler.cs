using Apps.UnbabelProjects.Invocables;
using Apps.UnbabelProjects.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.UnbabelProjects.DataSourceHandlers;

public class PipelineDataHandler : UnbabelProjectsInvocable, IAsyncDataSourceHandler
{
    public PipelineDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var endpoint = $"/pipelines/v0/customers/{CustomerId}/pipelines";
        var request = new RestRequest(endpoint);

        var response = await Client.Paginate<PipelineEntity>(request, Creds);

        return response
            .Where(x => context.SearchString is null ||
                        x.DisplayName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Id, x => x.DisplayName);
    }
}