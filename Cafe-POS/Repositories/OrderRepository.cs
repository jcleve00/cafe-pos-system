using Cafe_POS.Models;
using Cafe_POS.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Formats.Tar;

namespace Cafe_POS.Repositories;

public class OrderRepository : IOrderRepository
{
    private CafeContext _dbContext;
    private readonly AppConfig _appConfig;
    private IItemRepository _itemRepository;

    public OrderRepository(AppConfig config, IItemRepository itemRepository)
    {
        // Get the Connection string out of the AppConfiguration object
        // and put in the context
        _appConfig = config;
        _dbContext = new CafeContext(_appConfig.ConnectionString);
        _itemRepository = itemRepository;
    }
    public IEnumerable<CafeOrder> GetOpenOrders()
    {
        // Get all open orders
        return _dbContext.CafeOrders
            .Where(o => o.PaymentTypeId == null)
            .ToList();
            
    }
    public CafeOrder CreateOrder(int serverId)
    {
        // Create a new order and add it to database
        var order = new CafeOrder
        {
            ServerId = serverId,
            PaymentTypeId = null,
            OrderDate = DateTime.Now,
        };

        _dbContext.CafeOrders.Add(order);
        _dbContext.SaveChanges();
        return order;
    }
    public CafeOrder GetOrderDetails(int orderId)
    {
        var order = _dbContext.CafeOrders
            .Include(oi => oi.OrderItems)
            .ThenInclude(p => p.ItemPrice)
            .ThenInclude(i => i.Item)
            .Include(s => s.Server)
            .FirstOrDefault(o => o.OrderId == orderId);

        if (order == null)
        {
            throw new InvalidOperationException($"Order with id {orderId} not found.");
        }

        return order;
    }
    public CafeOrder AddItemToOrder(int orderId, int itemId, int quantity)
    {
        // Get current date and time
        DateTime orderDate = DateTime.Now;

        int timeOfDayId = _itemRepository.GetTimeOfDayId(orderDate);
        ItemPrice price = _itemRepository.GetItemPrice(itemId, DateOnly.FromDateTime(orderDate), timeOfDayId);
        // If price comes back null throw execption
        if (price == null)
        {
            throw new InvalidOperationException("No item price found.");
        }
        // Create an order item and add to database
        var orderItem = new OrderItem
        {
            OrderId = orderId,
            ItemPriceId = price.ItemPriceId,
            Quantity = (sbyte)quantity,
            ExtendedPrice = price.Price * quantity
        };

        _dbContext.Add(orderItem);
        _dbContext.SaveChanges();

        // Get the details of the current order
        var order = GetOrderDetails(orderId);
        // Get the subtotal of all the items so far
        order.SubTotal = order.OrderItems.Sum(o => o.ExtendedPrice);
        order.Tax = order.SubTotal * 0.0875m;
        order.Tip = order.SubTotal * .2m;
        order.AmountDue = order.SubTotal + order.Tax + order.Tip;
        _dbContext.SaveChanges();

        return order;
    }
    
    public CafeOrder UpdateOrderPaymentType(int orderId, int paymentTypeId)
    {
        var order = GetOrderDetails(orderId);
        order.PaymentTypeId = paymentTypeId;

        _dbContext.SaveChanges();
        return order;
    }
}