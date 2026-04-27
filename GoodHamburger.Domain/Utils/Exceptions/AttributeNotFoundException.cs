using GoodHamburger.Domain.Utils.Attributes;

namespace GoodHamburger.Domain.Utils.Exceptions
{
    [Serializable]
    [IgnoreDependencyInjectionAttribute]
    public class AttributeNotFoundException : ExceptionsRules
    {
        public AttributeNotFoundException(string attributeName) : base(attributeName + " inválido.") { }
    }
}