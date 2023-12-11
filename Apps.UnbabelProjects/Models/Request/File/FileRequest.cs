using Apps.UnbabelProjects.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.UnbabelProjects.Models.Request.File;

public class FileRequest
{
    [Display("Project")]
    [DataSource(typeof(ProjectDataHandler))]
    public string ProjectId { get; set; }
    
    [Display("File")]
    [DataSource(typeof(FileDataHandler))]
    public string FileId { get; set; }
}