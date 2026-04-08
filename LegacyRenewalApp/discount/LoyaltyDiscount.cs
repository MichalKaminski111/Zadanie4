using LegacyRenewalApp.discount;

namespace LegacyRenewalApp;

public class LoyaltyDiscount : ICustomerDiscount
{
    public CustomerDiscount Calculate(CustomerDiscountContext context)
    {
        if (context.YearsWithCompany >= 5) 
            return new CustomerDiscount(context.BaseAmount * 0.07m, "long-term loyalty discount; ");
        if (context.YearsWithCompany >= 2) 
            return new CustomerDiscount(context.BaseAmount * 0.03m, "basic loyalty discount; ");
        
        return new CustomerDiscount(0m, string.Empty);
    }
}