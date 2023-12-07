using Apps.UnbabelProjects.Api;
using Apps.UnbabelProjects.Constants;
using Apps.UnbabelProjects.Models.Entities;
using Apps.UnbabelProjects.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;

namespace Apps.UnbabelProjects.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        var creds = authenticationCredentialsProviders.ToArray();
        var customerId = creds.Get(CredsNames.CustomerId).Value;
        var client = new UnbabelProjectsClient();

        var endpoint = $"/projects/v0/customers/{customerId}/projects";
        var request = new RestRequest(endpoint);

        try
        {
            await client.ExecuteWithErrorHandling<PaginationResponse<ProjectEntity>>(request, creds);

            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}