using System.Collections.Generic;

namespace LegacyRenewalApp;

public class CustomerDiscountCalculator
{
    private readonly IEnumerable<ICustomerDiscount> _discounts;
    private readonly decimal minimumSubtotalAmount;

    public CustomerDiscountCalculator(IEnumerable<ICustomerDiscount> discounts)
    {
        _discounts = discounts;
    }

    public (decimal TotalDiscount, string AllNotes) CalculateAll(CustomerDiscountContext context)
    {
        decimal totalAmount = 0;
        string totalNotes = "";

        foreach (var strategy in _discounts)
        {
            var result = strategy.Calculate(context);
            totalAmount += result.Amount;
            totalNotes += result.Note;
        }
        
        decimal subtotalAfterDiscount = context.BaseAmount - totalAmount;
        if (subtotalAfterDiscount < minimumSubtotalAmount)
        {
            totalAmount = context.BaseAmount - minimumSubtotalAmount;
            totalNotes += "minimum discounted subtotal applied; ";
        }

        return (totalAmount, totalNotes);
    }
}