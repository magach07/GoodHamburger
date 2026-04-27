using GoodHamburger.Domain.Utils.Attributes;

namespace GoodHamburger.Domain.Utils.Exceptions
{
    [IgnoreDependencyInjectionAttribute]
    public class ExceptionsRules : Exception
    {
        public ExceptionsRules(string message) : base(message) { }

        public ExceptionsRules(string message, Exception innerException) : base(message, innerException) { }
    }
}