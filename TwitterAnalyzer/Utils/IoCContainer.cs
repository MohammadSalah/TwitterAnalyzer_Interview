using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
class IoCContainer : ScopeContainer, IDependencyResolver
{
    public IoCContainer(IUnityContainer container)
        : base(container)
    {
    }

    public IDependencyScope BeginScope()
    {
        var child = container.CreateChildContainer();
        return new ScopeContainer(child);
    }
}
