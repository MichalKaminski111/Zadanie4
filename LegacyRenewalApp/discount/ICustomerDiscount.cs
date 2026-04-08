using LegacyRenewalApp.discount;

namespace LegacyRenewalApp;

public interface ICustomerDiscount
{
    CustomerDiscount Calculate(CustomerDiscountContext context);
}