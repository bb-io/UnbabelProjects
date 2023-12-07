using Apps.UnbabelProjects.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.UnbabelProjects.Actions;

[ActionList]
public class FileActions : UnbabelProjectsInvocable
{
    public FileActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}