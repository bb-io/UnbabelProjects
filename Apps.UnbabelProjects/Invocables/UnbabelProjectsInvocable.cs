using Apps.UnbabelProjects.Api;
using Apps.UnbabelProjects.Constants;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.UnbabelProjects.Invocables;

public class UnbabelProjectsInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected UnbabelProjectsClient Client { get; }
    protected string CustomerId { get; }

    public UnbabelProjectsInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new();
        CustomerId = Creds.Get(CredsNames.CustomerId).Value;
    }
}