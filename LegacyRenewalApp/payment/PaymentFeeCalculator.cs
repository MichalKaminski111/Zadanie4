using System.Collections.Generic;

namespace LegacyRenewalApp.payment;

public class PaymentFeeCalculator
{
    private readonly Dictionary<string, IPaymentFee> _strategies = new()
    {
        { "CARD",          new CradPayment() },
        { "BANK_TRANSFER", new BankTransferPayment() },
        { "PAYPAL",        new PayPalPayment() },
        { "INVOICE",       new InvoicePayment() }
    };
    
    public PaymentFeeRecord CalculateTotalAmount(string method ,decimal amount)
    {
        var key = method.ToUpper();
        if (!_strategies.TryGetValue(key, out var strategy))
        {
            return new PaymentFeeRecord(0m, "unknown payment method; ");
        }

        return strategy.CalculateFee(amount);
    }
}