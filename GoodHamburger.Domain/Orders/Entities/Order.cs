using GoodHamburger.Domain.OrderItems.Entitites;
using GoodHamburger.Domain.OrdersStatus.Entities;
using GoodHamburger.Domain.Utils.Exceptions.Entities;

namespace GoodHamburger.Domain.Orders.Entities
{
    public class Order
    {
        public virtual int Id { get; protected set; }
        public virtual string CustomerName { get; protected set; } = null!;
        public virtual decimal Subtotal { get; protected set; }
        public virtual decimal Discount { get; protected set; }
        public virtual decimal TotalAmount { get; protected set; }
        public virtual int IdOrderStatus { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime UpdatedAt { get; protected set; }

        public virtual OrderStatus OrderStatus { get; protected set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = null!;


        protected Order() { }
        public Order(string customerName, int idOrderStatus)
        {
            SetCustomerName(customerName);
            SetIdOrderStatus(idOrderStatus);
            SetCreatedAt(DateTime.Now);
        }

        public virtual void SetCustomerName(string customerName)
        {
            customerName.ThrowInvalidAttributeIf(s => string.IsNullOrWhiteSpace(customerName), "Customer Name");
            CustomerName = customerName;
        }

        public virtual void SetSubtotal(decimal subtotal)
        {
            subtotal.ThrowInvalidAttributeIf(subtotal => subtotal < 0, "Subtotal");
            Subtotal = subtotal;
        }

        public virtual void SetDiscount(decimal discount)
        {
            discount.ThrowInvalidAttributeIf(discount => discount < 0, "Discount");
            Discount = discount;
        }

        public virtual void SetTotalAmount(decimal totalAmount)
        {
            totalAmount.ThrowInvalidAttributeIf(totalAmount => totalAmount < 0, "Total Amount");
            TotalAmount = totalAmount;
        }

        public virtual void SetIdOrderStatus(int idOrderStatus)
        {
            idOrderStatus.ThrowRecordNotFound("Order Status");
            IdOrderStatus = idOrderStatus;
        }

        public virtual void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        public virtual void SetUpdatedAt(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }
    }
}