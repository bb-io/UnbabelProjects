using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.UnbabelProjects.DataSourceHandlers.EnumDataHandlers;

public class ProjectStatusDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "created", "Created" },
        { "submitted", "Submitted" },
        { "estimating", "Estimating" },
        { "estimated", "Estimated" },
        { "estimate_expired", "Estimate expired" },
        { "in_progress", "In progress" },
        { "delivered", "Delivered" },
        { "canceled", "Canceled" },
        { "failed", "Failed" }
    };
}