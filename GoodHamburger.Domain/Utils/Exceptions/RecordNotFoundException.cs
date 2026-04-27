using GoodHamburger.Domain.Utils.Attributes;

namespace GoodHamburger.Domain.Utils.Exceptions
{
    [Serializable]
    [IgnoreDependencyInjectionAttribute]
    public class RecordNotFoundException : ExceptionsRules
    {
        public RecordNotFoundException(string message) : base(message + "informado não foi encontrato") { }
    }
}