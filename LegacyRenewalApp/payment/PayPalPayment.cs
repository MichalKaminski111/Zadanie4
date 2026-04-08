using LegacyRenewalApp.discount;

namespace LegacyRenewalApp.payment;

public class PayPalPayment : IPaymentFee
{
    public PaymentFeeRecord CalculateFee(decimal amount)
    {
        return new PaymentFeeRecord(amount * 0.035m, "paypal payment fee; ");
    }
}