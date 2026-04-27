using GoodHamburger.Application.Orders.Services.Interfaces;
using GoodHamburger.DataTransfer.MenuItems.Responses;
using GoodHamburger.DataTransfer.Order.Requests;
using GoodHamburger.DataTransfer.Orders.DTOs;
using GoodHamburger.DataTransfer.Orders.Responses;
using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.MenuItems.IRepositories;
using GoodHamburger.Domain.Orders.Commands;
using GoodHamburger.Domain.Orders.DTOs;
using GoodHamburger.Domain.Orders.Entities;
using GoodHamburger.Domain.Orders.Services.Interfaces;
using GoodHamburger.Domain.Utils.Exceptions.Entities;
using Mapster;

namespace GoodHamburger.Application.Orders.Services
{
    public class OrdersAppService : IOrdersAppService
    {
        private readonly IOrdersService _orderService;
        private readonly IMenuItemsRepository _menuItemRepository; 

        public OrdersAppService(IOrdersService orderService, IMenuItemsRepository menuItemRepository)
        {
            _orderService = orderService;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<bool> ValidateAndInsertOrderAndItemsAsync(OrderInsertRequest request, CancellationToken cancellationToken)
        {
            List<OrderItemsQuantityInsertDTO> orderItemsQuantityDto = request.ItemsQuantityOrder.Adapt<List<OrderItemsQuantityInsertDTO>>();

            try
            {
               _ = await ValidateDuplicatedItems(orderItemsQuantityDto, cancellationToken);

                IEnumerable<MenuItem> menuItemsOrder = await _menuItemRepository.GetByIdsAsync([..request.ItemsQuantityOrder.Select(it => it.Id)], cancellationToken);

                OrderInsertCommand command = new(request.CustomerName, menuItemsOrder);

                return await _orderService.ValidateAndInsertOrderAndItemsAsync(command, cancellationToken);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Order> orders = await _orderService.GetAllAsync(cancellationToken);

                return orders.Adapt<IEnumerable<OrderResponse>>();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<OrderSummaryResponse> GetSummaryAsync(int periodDays, CancellationToken cancellationToken)
        {       
            OrderSummaryDTO? orderSummary = await _orderService.GetSummaryAsync(periodDays, cancellationToken);

            return orderSummary!.Adapt<OrderSummaryResponse>();
        }

        public async Task<OrderResponse> SoftDeleteAsync(int id, CancellationToken cancellationToken)
        {
            Order order = await _orderService.SoftDeleteAsync(id, cancellationToken);

            return order.Adapt<OrderResponse>();
        }

        public async Task<ValidateOrderDto> ValidateOrderAndCalculateDiscount(OrderInsertRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<OrderItemsQuantityInsertDTO> orderItemsQuantityDto = request.ItemsQuantityOrder.Adapt<List<OrderItemsQuantityInsertDTO>>();

                _ = await ValidateDuplicatedItems(orderItemsQuantityDto, cancellationToken);

                IEnumerable<MenuItem> menuItemsOrder = await _menuItemRepository.GetByIdsAsync([.. request.ItemsQuantityOrder.Select(it => it.Id)], cancellationToken);

                OrderInsertCommand command = new(request.CustomerName, menuItemsOrder);

                Order order = await _orderService.ValidateOrderAsync(command, cancellationToken);

                return new ValidateOrderDto(order.Adapt<OrderResponse>(), menuItemsOrder.Adapt<IEnumerable<MenuItemResponse>>());
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> InsertOrderAndItemsAsync(ValidateOrderDto orderDto, CancellationToken cancellationToken)
        {
            try
            {
                ValidatedOrderCommand command = orderDto.Adapt<ValidatedOrderCommand>();
                return await _orderService.InsertOrderAndItemsAsync(command, cancellationToken);
            }
            catch(Exception)
            {
                throw;
            }
        }

        private async Task<bool> ValidateDuplicatedItems(List<OrderItemsQuantityInsertDTO> orderItemsQuantityDto, CancellationToken cancellationToken)
        {
            List<int> duplicatedItens = _orderService.ValidateItems(orderItemsQuantityDto);

            if (duplicatedItens.Count > 0)
            {
                List<MenuItem> duplicatedMenuItemsList = [.. await _menuItemRepository.GetByIdsAsync(duplicatedItens, cancellationToken)];

                var duplicatedNames = duplicatedMenuItemsList.Select(it => it.Name);

                orderItemsQuantityDto.ThrowDuplicatedItems("Itens", string.Join(", ", duplicatedNames));
            }
            else
            {
                List<MenuItem> itemsOrder = [.. await _menuItemRepository.GetByIdsAsync([.. orderItemsQuantityDto.Select(it => it.Id)], cancellationToken)];

                var duplicatedTypes = itemsOrder
                .GroupBy(io => io.IdMenuItemType)
                .Where(g => g.Count() > 1)
                .ToList();

                if (duplicatedTypes.Count > 0)
                {
                    var duplicatedNames = duplicatedTypes
                        .Select(g => g.First().MenuItemType.Description)
                        .ToList();


                    orderItemsQuantityDto.ThrowDuplicatedItems("Tipos", string.Join(", ", duplicatedNames));
                }
            }

            return true;
        }
    }
}