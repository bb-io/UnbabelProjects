using Blackbird.Applications.Sdk.Common;

namespace Apps.UnbabelProjects.Models.Entities;

public class ProjectEntity
{
    [Display("ID")]
    public string Id { get; set; }

    [Display("Customer ID")]
    public string CustomerId { get; set; }

    [Display("Name")]
    public string Name { get; set; }

    [Display("Status")]
    public string Status { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Completed at")]
    public DateTime? CompletedAt { get; set; }

    [Display("Pipeline IDs")]
    public IEnumerable<string> PipelineIds { get; set; }

    [Display("Requested by")]
    public string RequestedBy { get; set; }
}