using GoodHamburger.Application.Orders.Services.Interfaces;
using GoodHamburger.Application.ResultPattern;
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

        public async Task<AppResult<bool>> ValidateAndInsertOrderAndItemsAsync(OrderInsertRequest request, CancellationToken cancellationToken)
        {
            List<OrderItemsQuantityInsertDTO> orderItemsQuantityDto = request.ItemsQuantityOrder.Adapt<List<OrderItemsQuantityInsertDTO>>();

            try
            {
                _ = await ValidateDuplicatedItems(orderItemsQuantityDto, cancellationToken);

                IEnumerable<MenuItem> menuItemsOrder = await _menuItemRepository.GetByIdsAsync([.. request.ItemsQuantityOrder.Select(it => it.Id)], cancellationToken);

                OrderInsertCommand command = new(request.CustomerName, menuItemsOrder);

                return AppResult<bool>.Ok(await _orderService.ValidateAndInsertOrderAndItemsAsync(command, cancellationToken));
            }
            catch (Exception ex)
            {
                return AppResult<bool>.Fail(ex.Message);
            }
        }

        public async Task<AppResult<IEnumerable<OrderResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Order> orders = await _orderService.GetAllAsync(cancellationToken);

                return AppResult<IEnumerable<OrderResponse>>.Ok(orders.Adapt<IEnumerable<OrderResponse>>());
            }
            catch (Exception ex)
            {
                return AppResult<IEnumerable<OrderResponse>>.Fail(ex.Message);
            }
        }

        public async Task<AppResult<OrderSummaryResponse>> GetSummaryAsync(int periodDays, CancellationToken cancellationToken)
        {
            try
            {
                OrderSummaryDTO? orderSummary = await _orderService.GetSummaryAsync(periodDays, cancellationToken);

                return AppResult<OrderSummaryResponse>.Ok(orderSummary!.Adapt<OrderSummaryResponse>());
            }
            catch (Exception ex)
            {
                return AppResult<OrderSummaryResponse>.Fail(ex.Message);
            }
        }

        public async Task<AppResult<OrderResponse>> SoftDeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                Order order = await _orderService.SoftDeleteAsync(id, cancellationToken);

                return AppResult<OrderResponse>.Ok(order.Adapt<OrderResponse>());
            }
            catch (Exception ex)
            {
                return AppResult<OrderResponse>.Fail(ex.Message);
            }
        }

        public async Task<AppResult<ValidateOrderDto>> ValidateOrderAndCalculateDiscount(OrderInsertRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<OrderItemsQuantityInsertDTO> orderItemsQuantityDto = request.ItemsQuantityOrder.Adapt<List<OrderItemsQuantityInsertDTO>>();

                _ = await ValidateDuplicatedItems(orderItemsQuantityDto, cancellationToken);

                IEnumerable<MenuItem> menuItemsOrder = await _menuItemRepository.GetByIdsAsync([.. request.ItemsQuantityOrder.Select(it => it.Id)], cancellationToken);

                OrderInsertCommand command = new(request.CustomerName, menuItemsOrder);

                Order order = await _orderService.ValidateOrderAsync(command, cancellationToken);

                return AppResult<ValidateOrderDto>.Ok(new ValidateOrderDto(order.Adapt<OrderResponse>(), menuItemsOrder.Adapt<IEnumerable<MenuItemResponse>>()));
            }
            catch (Exception ex)
            {
                return AppResult<ValidateOrderDto>.Fail(ex.Message);
            }
        }

        public async Task<AppResult<bool>> InsertOrderAndItemsAsync(ValidateOrderDto orderDto, CancellationToken cancellationToken)
        {
            try
            {
                ValidatedOrderCommand command = orderDto.Adapt<ValidatedOrderCommand>();
                return AppResult<bool>.Ok(await _orderService.InsertOrderAndItemsAsync(command, cancellationToken));
            }
            catch (Exception ex)
            {
                return AppResult<bool>.Fail(ex.Message);
            }
        }

        #region Duplicated items/types validator
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
        #endregion
    }
}