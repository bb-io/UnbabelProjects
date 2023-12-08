using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.UnbabelProjects.DataSourceHandlers.EnumDataHandlers;

public class OrderStatusDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "created", "Created" },
        { "services_pre_pipeline", "Services pre pipeline" },
        { "services_post_pipeline", "Services post pipeline" },
        { "in_progress", "In progress" },
        { "completed", "Completed" },
        { "failed", "Failed" },
    };
}