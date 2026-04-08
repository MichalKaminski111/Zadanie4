namespace LegacyRenewalApp;

public record CustomerDiscountContext
(
    string Segment,
    int YearsWithCompany,
    int LoyaltyPoints,
    decimal BaseAmount,
    bool IsEducationEligible,
    decimal SeatCount
);