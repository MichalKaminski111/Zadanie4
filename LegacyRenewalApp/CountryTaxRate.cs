using System.Collections.Generic;

namespace LegacyRenewalApp;

public class CountryTaxRate
{
    private static readonly Dictionary<string, decimal> CountryTaxRates = new Dictionary<string, decimal>
    {
        { "Poland", 0.23m },
        { "Germany", 0.19m },
        { "Czech Republic", 0.21m },
        { "Norway", 0.25m }
    };

    public static decimal GetTaxRate(string countryCode)
    {
        if (CountryTaxRates.TryGetValue(countryCode.ToUpperInvariant(), out decimal taxRate))
        {
            return taxRate;
        }

        return 0.20m;
    }
}