using LegacyRenewalApp.discount;

namespace LegacyRenewalApp.payment;

public class InvoicePayment : IPaymentFee
{
    public PaymentFeeRecord CalculateFee(decimal amount)
    {
        return new PaymentFeeRecord(amount * 0.00m, "invoice payment; ");
    }
}