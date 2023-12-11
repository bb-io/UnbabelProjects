using Apps.UnbabelProjects.Models.Entities;

namespace Apps.UnbabelProjects.Models.Response.File;

public class UploadFileResponse : FileEntity
{
    public string UploadUrl { get; set; }
}