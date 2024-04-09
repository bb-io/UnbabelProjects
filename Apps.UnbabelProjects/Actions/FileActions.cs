using Apps.UnbabelProjects.Constants;
using Apps.UnbabelProjects.Extensions;
using Apps.UnbabelProjects.Invocables;
using Apps.UnbabelProjects.Models.Entities;
using Apps.UnbabelProjects.Models.Request.File;
using Apps.UnbabelProjects.Models.Request.Project;
using Apps.UnbabelProjects.Models.Response.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.UnbabelProjects.Actions;

[ActionList]
public class FileActions : UnbabelProjectsInvocable
{
    private readonly IFileManagementClient _fileManagementClient;
    public FileActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
    {
        _fileManagementClient = fileManagementClient;
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
        [ActionParameter] UploadFileInput input,
        [ActionParameter] FileContentRequest file)
    {
        var request = new RestRequest($"/projects/v0/customers/{CustomerId}/projects/{project.ProjectId}/files",
                Method.Post)
            .WithJsonBody(new UploadFileRequest(input, file), JsonConfig.Settings);
        var response = await Client.ExecuteWithErrorHandling<UploadFileResponse>(request, Creds);

        var fileBytes = _fileManagementClient.DownloadAsync(file.File).Result.GetByteData().Result;
        var uploadRequest = new RestRequest(response.UploadUrl, Method.Put)
            .AddFile("file", fileBytes, file.FileName ?? file.File.Name);
        var uploadResponse = await new RestClient().ExecuteAsync(uploadRequest);

        if (!uploadResponse.IsSuccessStatusCode)
            throw new(uploadResponse.Content);

        return response;
    }

    [Action("Download file", Description = "Download content of a specific project file")]
    public async Task<FileResponse> DownloadFile([ActionParameter] FileRequest fileRequest)
    {
        var file = await GetFile(fileRequest);

        if (file.DownloadUrl is null)
            throw new("File does not have content yet");

        var downloadResponse = await DownloadFileContent(file.DownloadUrl);

        var contentTypeHeader =
            downloadResponse.ContentHeaders!.First(x => x.Name == "Content-Type").Value!.ToString()!;
        var fileContent = await downloadResponse.RawBytes!.ReadFromMultipartFormData(contentTypeHeader);

        using var stream = new MemoryStream(fileContent);
        var fileResult = await _fileManagementClient.UploadAsync(stream, MimeTypes.GetMimeType(file.Name), file.Name);

        return new(fileResult);
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

    private async Task<RestResponse> DownloadFileContent(string fileDownloadUrl)
    {
        var response = await new RestClient().ExecuteAsync(new(fileDownloadUrl));

        if (!response.IsSuccessStatusCode)
            throw new($"Failed to download file from {fileDownloadUrl}; Response: {response.Content}");

        return response;
    }
}