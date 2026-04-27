using GoodHamburger.DataTransfer.Orders.Requests;
using System.ComponentModel.DataAnnotations;

namespace GoodHamburger.DataTransfer.Order.Requests
{
    public class OrderInsertRequest
    {
        [Required(ErrorMessage = "Informe o nome do cliente")]
        public string CustomerName { get; set; } = null!;
        public List<OrderItemsQuantityInsertRequest> ItemsQuantityOrder { get; set; } = null!;
    }
}
