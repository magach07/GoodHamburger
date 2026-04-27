using GoodHamburger.Domain.Utils.Attributes;

namespace GoodHamburger.Domain.Utils.Exceptions
{
    [Serializable]
    [IgnoreDependencyInjectionAttribute]
    public class UserObjectDuplicatedException : ExceptionsRules
    {
        public UserObjectDuplicatedException(string attributeName) : base(attributeName + " já cadastrado no sistema.") { }
    }
}