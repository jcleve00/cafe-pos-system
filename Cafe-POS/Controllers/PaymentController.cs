using Cafe_POS.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_POS.Controllers;

public class PaymentController : Controller
{
    private readonly IPaymentTypeRepository _paymentTypeRepository;
    private readonly IOrderRepository _orderRepository;

    public PaymentController(IPaymentTypeRepository paymentTypeRepository, IOrderRepository orderRepository)
    {
        _paymentTypeRepository = paymentTypeRepository;
        _orderRepository = orderRepository;
    }

    //show every availabe payment type for a order
    public IActionResult ProcessPayment(int orderId)
    {
        //stash orderId in Viewbag so the view can build the form back to this order
        ViewBag.OrderId = orderId;

        var paymentTypes = _paymentTypeRepository.GetAllPaymentTypes();
        return View(paymentTypes);
    }

    [HttpPost]
    public IActionResult CompletePayment(int orderId, int paymentTypeId)
    {
        _orderRepository.UpdateOrderPaymentType(orderId, paymentTypeId);

        //Order is now closed, send user back home
        return RedirectToAction("Index", "Home");
    }
}