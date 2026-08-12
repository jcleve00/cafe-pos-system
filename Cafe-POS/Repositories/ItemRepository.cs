using Cafe_POS.Models;
using Cafe_POS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cafe_POS.Repositories;

public class ItemRepository : IItemRepository
{
    private CafeContext _dbContext;
    private readonly AppConfig _appConfig;

    public ItemRepository(AppConfig config)
    {
        // Get the Connection string out of the AppConfiguration object
        // and put in the context
        _appConfig = config;
        _dbContext = new CafeContext(_appConfig.ConnectionString);
    }

    public ItemPrice GetItemPrice(int itemId, DateOnly orderDate, int timeOfDayId)
    {
        return _dbContext.ItemPrices
            .Where(i => i.ItemId == itemId)
            .Where(t => t.TimeOfDayId == timeOfDayId)
            .Where(o => o.StartDate <= orderDate && (orderDate <= o.EndDate || o.EndDate == null))
            .FirstOrDefault()!;
    }
    public int GetTimeOfDayId(DateTime orderTime)
    {
        int hour = orderTime.Hour;

        if (hour >= 6 && hour < 11)
        {
            return 1;
        }
        else if (hour >= 11 && hour < 14)
        {
            return 2;
        }
        else if (hour >= 14 && hour < 17)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    public IEnumerable<Item> GetAvailableItems(DateTime orderTime)
    {
        DateOnly orderDate = DateOnly.FromDateTime(orderTime);
        int TimeOfDayId = GetTimeOfDayId(orderTime);


        //Keeps items only if at least one price row matches the current time of day and date range
        return _dbContext.Items
            .Where(i => i.ItemPrices.Any(ip =>
                ip.TimeOfDayId == TimeOfDayId &&
                ip.StartDate <= orderDate &&
                (ip.EndDate == null || ip.EndDate >= orderDate)))
                .Include(c => c.Category) // Added include Category
            .ToList();
    }
}