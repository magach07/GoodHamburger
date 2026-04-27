
namespace GoodHamburger.Domain.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class IgnoreDependencyInjectionAttribute : Attribute { }
}
