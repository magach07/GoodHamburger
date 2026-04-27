using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.OrderItems.Entitites;
using GoodHamburger.Domain.OrderItems.IRepositories;
using GoodHamburger.Domain.Orders.Commands;
using GoodHamburger.Domain.Orders.DTOs;
using GoodHamburger.Domain.Orders.Entities;
using GoodHamburger.Domain.Orders.IRepositories;
using GoodHamburger.Domain.Orders.Services.Interfaces;
using GoodHamburger.Domain.OrdersStatus.Entities;
using GoodHamburger.Domain.OrdersStatus.IRepositories;
using GoodHamburger.Domain.Utils.Exceptions.Entities;

namespace GoodHamburger.Domain.Orders.Services
{
    public class OrdersService : IOrdersService
    {
        const int ORDER_STATUS_SOLICITADO = 1;
        const int ORDER_STATUS_CANCELADO = 3;

        const int MENU_ITEM_SANDUICHE = 1;
        const int MENU_ITEM_BATATA = 2;
        const int MENU_ITEM_REFRIGERANTE = 3;

        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IOrdersStatusRepository _orderStatusRepository;

        public OrdersService(IOrdersRepository ordersRepository, IOrderItemsRepository orderItemsRepository, IOrdersStatusRepository orderStatusRepository)
        {
            _ordersRepository = ordersRepository;
            _orderItemsRepository = orderItemsRepository;
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<Order> ValidateOrderAsync(OrderInsertCommand command, CancellationToken cancellationToken)
        {
            OrderStatus? orderStatus = await _orderStatusRepository.GetByIdAsync(ORDER_STATUS_SOLICITADO, cancellationToken);
            if (orderStatus is null)
                orderStatus.ThrowRecordNotFound($"Order status não encontrado. ID: {ORDER_STATUS_SOLICITADO}.");

            Order order = new(command.CustomerName, orderStatus!.Id);

            return CalculateDiscount(order, command.MenuItemsOrder);
        }

        public async Task<bool> InsertOrderAndItemsAsync(ValidatedOrderCommand command, CancellationToken cancellationToken)
        {
            await _ordersRepository.InsertAsync(command.Order, cancellationToken);

            IEnumerable<OrderItem> orderItems = command.MenuItemsOrder.Select(i => new OrderItem(command.Order, i));

            await _orderItemsRepository.InsertEnumerableAsync(orderItems, cancellationToken);

            return true;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken) =>
            await _ordersRepository.GetAllAsync(cancellationToken);

        public async Task<OrderSummaryDTO> GetSummaryAsync(int periodDays, CancellationToken cancellationToken)
        {
            OrderSummaryDTO? orderSummary = await _ordersRepository.GetSummaryAsync(periodDays, cancellationToken);

            orderSummary ??= new OrderSummaryDTO
            {
                TotalOrders = 0,
                SubtotalAmount = 0,
                TotalAmount = 0,
                TotalDiscount = 0
            };

            return orderSummary;
        }

        public async Task<Order> SoftDeleteAsync(int id, CancellationToken cancellationToken)
        {
            Order? order = await _ordersRepository.GetByIdAsync(id, cancellationToken);
            if (order is null)
                order.ThrowRecordNotFound($"Pedido não encontrado. ID: {id}");

            OrderStatus? orderStatus = await _orderStatusRepository.GetByIdAsync(ORDER_STATUS_CANCELADO, cancellationToken);

            order!.SetIdOrderStatus(orderStatus!.Id);

            return await _ordersRepository.SoftDeleteAsync(order!, cancellationToken);
        }

        #region Método de validação e inserção completa disponível apenas para teste direto via backend.
        // Esse método não existiria. Existe apenas para disponibilizar endpoint teste via Backend
        public async Task<bool> ValidateAndInsertOrderAndItemsAsync(OrderInsertCommand command, CancellationToken cancellationToken)
        {
            OrderStatus? orderStatus = await _orderStatusRepository.GetByIdAsync(ORDER_STATUS_SOLICITADO, cancellationToken);
            if (orderStatus is null)
                orderStatus.ThrowRecordNotFound($"Order status não encontrado. ID: {ORDER_STATUS_SOLICITADO}.");

            Order order = new(command.CustomerName, orderStatus!.Id);

            CalculateDiscount(order, command.MenuItemsOrder);

            await _ordersRepository.InsertAsync(order, cancellationToken);

            IEnumerable<OrderItem> orderItems = command.MenuItemsOrder.Select(i => new OrderItem(order, i));

            await _orderItemsRepository.InsertEnumerableAsync(orderItems, cancellationToken);

            return true;
        }
        #endregion

        #region Business Rules

        public List<int> ValidateItems(IEnumerable<OrderItemsQuantityInsertDTO> orderItemsQuantity)
        {
            List<int> duplicatedItems = [.. orderItemsQuantity.Where(it => it.Quantity > 1).Select(it => it.Id)];

            return duplicatedItems.Count > 0 ? duplicatedItems : [];
        }

        private static Order CalculateDiscount(Order order, IEnumerable<MenuItem> menuItemsOrder)
        {
            MenuItem? hasSandwich = menuItemsOrder.Where(i => i.IdMenuItemType == MENU_ITEM_SANDUICHE).FirstOrDefault();
            MenuItem? hasFries = menuItemsOrder.Where(i => i.IdMenuItemType == MENU_ITEM_BATATA).FirstOrDefault();
            MenuItem? hasDrink = menuItemsOrder.Where(i => i.IdMenuItemType == MENU_ITEM_REFRIGERANTE).FirstOrDefault();

            decimal subtotal = menuItemsOrder.Sum(oi => oi.Price);

            decimal discount = (hasSandwich, hasFries, hasDrink) switch
            {
                (not null, not null, not null) => subtotal * 0.20m,
                (not null, null, not null) => subtotal * 0.15m,
                (not null, not null, null) => subtotal * 0.10m,
                _ => 0
            };

            order.SetSubtotal(subtotal);
            order.SetDiscount(discount);
            order.SetTotalAmount(subtotal - discount);

            return order;
        }

        #endregion
    }
}