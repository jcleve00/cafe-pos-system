using System.Net.Security;
using Cafe_POS.Models;

namespace Cafe_POS.Interfaces;

public interface IOrderRepository
{
    IEnumerable<CafeOrder> GetOpenOrders();
    CafeOrder CreateOrder(int serverId);
    CafeOrder GetOrderDetails(int orderId);
    CafeOrder AddItemToOrder(int orderId, int itemId, int quantity);
    CafeOrder UpdateOrderPaymentType(int orderId, int paymentTypeId);
}