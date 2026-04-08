using LegacyRenewalApp.discount;

namespace LegacyRenewalApp;

public class SegmentDiscount : ICustomerDiscount
{
    public CustomerDiscount Calculate(CustomerDiscountContext context)
    {
        var (amount, note) = context.Segment switch
        {
            "Silver" => (context.BaseAmount * 0.05m, "silver discount; "),
            "Gold" => (context.BaseAmount * 0.10m, "gold discount; "),
            "Platinum" => (context.BaseAmount * 0.15m, "platinum discount; "),
            "Education" when context.IsEducationEligible => (context.BaseAmount * 0.20m, "education discount; "),
            _ => (0m, string.Empty)
        };
        return new CustomerDiscount(amount, note);
    }
}