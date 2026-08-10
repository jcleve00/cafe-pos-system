using System.Net.Security;
using Cafe_POS.Models;

namespace Cafe_POS.Interfaces;

public interface IPaymentTypeRepository
{
    IEnumerable<PaymentType> GetAllPaymentTypes();
    PaymentType GetPaymentTypeById(int paymentTypeId);
}