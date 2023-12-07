using Blackbird.Applications.Sdk.Common;

namespace Apps.UnbabelProjects;

public class UnbabelProjectsApplication : IApplication
{
    public string Name
    {
        get => "Unbabel Projects";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}