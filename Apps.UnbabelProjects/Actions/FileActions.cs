using Apps.UnbabelProjects.Constants;
using Apps.UnbabelProjects.Invocables;
using Apps.UnbabelProjects.Models.Entities;
using Apps.UnbabelProjects.Models.Request.File;
using Apps.UnbabelProjects.Models.Request.Project;
using Apps.UnbabelProjects.Models.Response.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Utilities;
using RestSharp;

namespace Apps.UnbabelProjects.Actions;

[ActionList]
public class FileActions : UnbabelProjectsInvocable
{
    public FileActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List files", Description = "List all project files")]
    public async Task<ListFilesResponse> ListFiles([ActionParameter] ProjectRequest input)
    {
        var request = new RestRequest($"/projects/v0/customers/{CustomerId}/projects/{input.ProjectId}/files");
        var items = await Client.Paginate<FileEntity>(request, Creds);

        return new(items);
    }

    [Action("Upload file", Description = "Upload a new file to the project")]
    public async Task<FileEntity> UploadFile(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] UploadFileRequest input)
    {
        var request = new RestRequest($"/projects/v0/customers/{CustomerId}/projects/{project.ProjectId}/files",
                Method.Post)
            .WithJsonBody(input, JsonConfig.Settings);
        var response = await Client.ExecuteWithErrorHandling<UploadFileResponse>(request, Creds);

        var uploadRequest = new RestRequest(response.UploadUrl, Method.Post)
            .AddFile("file", "my text file"u8.ToArray(), "myfile1.txt");
        var uploadResponse = await new RestClient().ExecuteAsync(uploadRequest);

        return response;
    }

    [Action("Download file", Description = "Download content of a specific project file")]
    public async Task<FileResponse> DownloadFile([ActionParameter] FileRequest fileRequest)
    {
        var file = await GetFile(fileRequest);

        if (file.DownloadUrl is null)
            throw new("File does not have content yet");

        var fileContent = await FileDownloader.DownloadFileBytes(file.DownloadUrl);
        fileContent.Name = file.Name;

        return new(fileContent);
    }

    [Action("Get file", Description = "Get details of a specific file")]
    public Task<FileEntity> GetFile([ActionParameter] FileRequest file)
    {
        var endpoint = $"/projects/v0/customers/{CustomerId}/projects/{file.ProjectId}/files/{file.FileId}";
        var request = new RestRequest(endpoint);

        return Client.ExecuteWithErrorHandling<FileEntity>(request, Creds);
    }

    [Action("Delete file", Description = "Delete specific file from the project")]
    public Task DeleteFile([ActionParameter] FileRequest file)
    {
        var endpoint = $"/projects/v0/customers/{CustomerId}/projects/{file.ProjectId}/files/{file.FileId}";
        var request = new RestRequest(endpoint, Method.Delete);

        return Client.ExecuteWithErrorHandling(request, Creds);
    }
}