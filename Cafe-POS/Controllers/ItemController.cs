using Cafe_POS.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_POS.Controllers;

public class ItemController : Controller
{
    private readonly IItemRepository _itemRepository;
    private readonly IOrderRepository _orderRepository;

    public ItemController(IItemRepository itemRepository, IOrderRepository orderRepository)
    {
        _itemRepository = itemRepository;
        _orderRepository = orderRepository;
    }

    //shows items abailable to add to an order
    public IActionResult AddItems(int orderId)
    {
        //stash order in viewbag so the view can build links/forms back to this same order
        ViewBag.OrderId = orderId;

        var items = _itemRepository.GetAvailableItems(DateTime.Now);
        return View(items);
    }

    //add selected item and quantity to the order
    [HttpPost]
    public IActionResult AddItem(int orderId, int itemId, int quantity)
    {
        _orderRepository.AddItemToOrder(orderId, itemId, quantity );

        //Redirect controller
        return RedirectToAction("OrderDetails", "Order", new {orderId = orderId});
    }
}