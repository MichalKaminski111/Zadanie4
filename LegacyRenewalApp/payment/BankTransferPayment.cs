using LegacyRenewalApp.discount;

namespace LegacyRenewalApp.payment;

public class BankTransferPayment : IPaymentFee
{
    public PaymentFeeRecord CalculateFee(decimal amount)
    {
        return new PaymentFeeRecord(amount * 0.02m, "bank transfer fee; ");
    }
}