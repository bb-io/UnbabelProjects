using Apps.UnbabelProjects.Constants;
using Apps.UnbabelProjects.Extensions;
using Apps.UnbabelProjects.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.UnbabelProjects.Api;

public class UnbabelProjectsClient : BlackBirdRestClient
{
    protected override JsonSerializerSettings? JsonSettings => JsonConfig.Settings;

    public UnbabelProjectsClient() : base(new()
    {
        BaseUrl = Urls.Api.ToUri()
    })
    {
    }

    public async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request,
        AuthenticationCredentialsProvider[] creds)
    {
        var token = await creds.GetAccessToken();
        request.AddHeader("Authorization", $"Bearer {token.AccessToken}");

        return await ExecuteWithErrorHandling(request);
    }

    public async Task<T> ExecuteWithErrorHandling<T>(RestRequest request, AuthenticationCredentialsProvider[] creds)
    {
        var token = await creds.GetAccessToken();
        request.AddHeader("Authorization", $"Bearer {token.AccessToken}");

        return await ExecuteWithErrorHandling<T>(request);
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (string.IsNullOrWhiteSpace(response.Content))
            return new("Something went wrong");
        
        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content, JsonSettings)!;

        if (errorResponse.Message is not null)
            return new(errorResponse.Message);

        if (errorResponse.ErrorDescription is not null)
            return new(errorResponse.ErrorDescription);

        if (errorResponse.Detail is not null)
            return new(errorResponse.Detail);

        return new("Something went wrong");
    }

    public async Task<List<T>> Paginate<T>(RestRequest request, AuthenticationCredentialsProvider[] creds,
        JObject payload, int? limit = default)
    {
        string? nextToken = null;

        request.Resource = request.Resource.SetQueryParameter("page_size", (limit ?? 50).ToString());
        var result = new List<T>();
        do
        {
            payload["page_token"] = nextToken;
            request.WithJsonBody(payload, JsonConfig.Settings);

            var response = await ExecuteWithErrorHandling<PaginationResponse<T>>(request, creds);

            nextToken = response.NextPageToken;
            result.AddRange(response.Results);

            if (limit != default && result.Count >= limit)
                return result;
            
        } while (!string.IsNullOrWhiteSpace(nextToken));

        return result;
    }

    public async Task<List<T>> Paginate<T>(RestRequest request, AuthenticationCredentialsProvider[] creds)
    {
        string? nextToken = null;
        var baseUrl = request.Resource;

        var result = new List<T>();
        do
        {
            if (nextToken is not null)
                request.Resource = baseUrl.SetQueryParameter("page_token", nextToken);

            var response = await ExecuteWithErrorHandling<PaginationResponse<T>>(request, creds);

            nextToken = response.NextPageToken;
            result.AddRange(response.Results);
        } while (!string.IsNullOrWhiteSpace(nextToken));

        return result;
    }
}