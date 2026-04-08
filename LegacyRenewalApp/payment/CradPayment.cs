using LegacyRenewalApp.discount;

namespace LegacyRenewalApp.payment;

public class CradPayment : IPaymentFee
{
    public PaymentFeeRecord CalculateFee(decimal amount)
    {
        return new PaymentFeeRecord(amount * 0.01m, "card payment fee; ");
    }
}