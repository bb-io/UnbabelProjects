using Apps.UnbabelProjects.DataSourceHandlers;
using Apps.UnbabelProjects.DataSourceHandlers.EnumDataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.UnbabelProjects.Models.Request.Order;

public class SearchOrdersRequest
{
    public string? Name { get; set; }
    
    [Display("Pipelines")]
    [DataSource(typeof(PipelineDataHandler))]
    public IEnumerable<string>? PipelineIds { get; set; }
    
    public IEnumerable<string>? Extension { get; set; }
    
    [DataSource(typeof(OrderStatusDataHandler))]
    public IEnumerable<string>? Status { get; set; }
}