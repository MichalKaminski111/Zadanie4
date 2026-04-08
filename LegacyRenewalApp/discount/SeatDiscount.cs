using LegacyRenewalApp.discount;

namespace LegacyRenewalApp;

public class SeatDiscount : ICustomerDiscount
{
    public CustomerDiscount Calculate(CustomerDiscountContext context)
    {
        if (context.SeatCount >= 50)
            return new CustomerDiscount(context.BaseAmount * 0.12m, "large team discount; ");
        if (context.SeatCount >= 20)
            return new CustomerDiscount(context.BaseAmount * 0.08m, "medium team discount; ");
        if (context.SeatCount >= 10)
            return new CustomerDiscount(context.BaseAmount * 0.04m, "small team discount; ");
        
        return new CustomerDiscount(0m, string.Empty);
    }
}