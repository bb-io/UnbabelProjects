using Apps.UnbabelProjects.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.UnbabelProjects.Models.Request.Project;

public class CreateProjectRequest
{
    public string Name { get; set; }
    
    [Display("Pipelines")]
    [DataSource(typeof(PipelineDataHandler))]
    public IEnumerable<string> PipelineIds { get; set; }
    
    [Display("Email of request creator")]
    public string RequestedBy { get; set; }
}