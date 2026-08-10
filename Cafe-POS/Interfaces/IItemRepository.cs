using System.Net.Security;
using Cafe_POS.Models;

namespace Cafe_POS.Interfaces;

public interface IItemRepository
{
    ItemPrice GetItemPrice(int itemId, DateOnly orderDate, int timeOfDayId);
    int GetTimeOfDayId(DateTime orderTime);
}