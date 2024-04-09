using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.UnbabelProjects;

public class UnbabelProjectsApplication : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.MachineTranslationAndMtqe];
        set { }
    }
    
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