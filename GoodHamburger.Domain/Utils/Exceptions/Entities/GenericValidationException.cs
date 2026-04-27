using GoodHamburger.Domain.Utils.Attributes;

namespace GoodHamburger.Domain.Utils.Exceptions.Entities
{
    [IgnoreDependencyInjectionAttribute]
    public static class GenericValidationException
    {
        public static void ThrowInvalidAttributeIf<T>(this T entity, Func<T, bool> expression, string attributeDescription)
        {
            if (expression(entity))
                throw new AttributeNotFoundException(attributeDescription);
        }

        public static void ThrowRecordNotFound<T>(this T entity, string message)
        {
            if (entity == null)
                throw new RecordNotFoundException(message);
        }

        public static void ThrowDuplicatedItems<T>(this T entity, string typeOrItem, string name)
        {
            if (entity is not null)
                throw new DuplicatedItemsException(typeOrItem, name);
        }
    }
}