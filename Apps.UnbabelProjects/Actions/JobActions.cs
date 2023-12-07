using Apps.UnbabelProjects.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.UnbabelProjects.Actions;

[ActionList]
public class JobActions : UnbabelProjectsInvocable
{
    public JobActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}