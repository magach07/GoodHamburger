using GoodHamburger.Domain.Utils.Attributes;

namespace GoodHamburger.Domain.Utils.Exceptions
{
    [Serializable]
    [IgnoreDependencyInjectionAttribute]
    public class DuplicatedItemsException : ExceptionsRules
    {
        public DuplicatedItemsException(string typeOrItem, string name) : base($"{typeOrItem} duplicados: não é possível inserir mais de um(a) {name}") { }
    }
}