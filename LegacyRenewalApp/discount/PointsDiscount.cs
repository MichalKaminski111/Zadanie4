using LegacyRenewalApp.discount;

namespace LegacyRenewalApp;

public class PointsDiscount : ICustomerDiscount
{
    private readonly bool _useLoyaltyPoints;

    public PointsDiscount(bool useLoyaltyPoints)
    {
        _useLoyaltyPoints = useLoyaltyPoints;
    }

    public CustomerDiscount Calculate(CustomerDiscountContext context)
    {
        if (!_useLoyaltyPoints || context.LoyaltyPoints <= 0)
        {
            return new CustomerDiscount(0m, string.Empty);
        }

        int pointsToUse = context.LoyaltyPoints > 200 ? context.LoyaltyPoints : 200;
        
        return new CustomerDiscount(
            (decimal)pointsToUse, 
            $"loyalty points used: {pointsToUse}; "
        );
    }
}