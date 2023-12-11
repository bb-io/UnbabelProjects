using Blackbird.Applications.Sdk.Common;

namespace Apps.UnbabelProjects.Models.Request.File;

public class FileContentRequest
{
    public Blackbird.Applications.Sdk.Common.Files.File File { get; set; }
    
    [Display("File name")]
    public string? FileName { get; set; }
}