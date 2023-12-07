using Apps.UnbabelProjects.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.UnbabelProjects.Actions;

[ActionList]
public class OrderActions : UnbabelProjectsInvocable
{
    public OrderActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}