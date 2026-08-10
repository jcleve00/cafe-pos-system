using System.ComponentModel.Design;
using Cafe_POS.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_POS.Controllers;

public class HomeController : Controller
{
    private readonly IOrderRepository _orderRepository;

    public HomeController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public IActionResult Index()
    {
        //get every order that doesnt have a payment type yet
        var openOrders = _orderRepository.GetOpenOrders();

        //Hand that list to the matching view
        return View(openOrders);
    }
}