namespace LegacyRenewalApp.payment;

public interface IPaymentFee
{
    PaymentFeeRecord CalculateFee(decimal amount);
}