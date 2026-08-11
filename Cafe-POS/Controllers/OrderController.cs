using System.ComponentModel.Design;
using Cafe_POS.Interfaces;
using Cafe_POS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cafe_POS.Controllers;

public class OrderController : Controller
{
    private readonly IServerRepository _serverRepository;
    private readonly IOrderRepository _orderRepository;
    public OrderController(IOrderRepository orderRepository, IServerRepository serverRepository)
    {
        _orderRepository = orderRepository;
        _serverRepository = serverRepository;
    }

    public IActionResult SelectServer()
    {
        // Get active servers
        var activeServers = _serverRepository.GetActiveServers();
        // Hand list to view
        return View(activeServers);
    }
    public IActionResult CreateNew(int serverId)
    {
        // Create new order
        var order = _orderRepository.CreateOrder(serverId);

        // Redirect to details page
        return RedirectToAction("OrderDetails", new {orderId = order.OrderId});
    }
    public IActionResult OrderDetails(int orderId)
    {
        var order = _orderRepository.GetOrderDetails(orderId);

        return View(order);
    }
}