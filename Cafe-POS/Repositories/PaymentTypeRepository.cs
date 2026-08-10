using Cafe_POS.Models;
using Cafe_POS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cafe_POS.Repositories;

public class PaymentTypeRepository : IPaymentTypeRepository
{
    //Hold EF Core "Database Connection" for this repo
    private CafeContext _dbContext;

    //hold app's configuration
    private readonly AppConfig _appConfig;

    public PaymentTypeRepository(AppConfig config)
    {
        _appConfig = config;

        //create new database context
        _dbContext = new CafeContext(_appConfig.ConnectionString);
    }

    //return every roy in payment type table
    public IEnumerable<PaymentType> GetAllPaymentTypes()
    {
        return _dbContext.PaymentTypes
            .ToList();
    }

    public PaymentType GetPaymentTypeById(int paymentTypeId)
    {
        //FirstOrDefault returns the first instance where PaymentTypeId matches
        return _dbContext.PaymentTypes
            .FirstOrDefault(p => p.PaymentTypeId == paymentTypeId);
    }
}