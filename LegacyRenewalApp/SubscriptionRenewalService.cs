using System;
using LegacyRenewalApp.payment;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            if (customerId <= 0)
            {
                throw new ArgumentException("Customer id must be positive");
            }

            if (string.IsNullOrWhiteSpace(planCode))
            {
                throw new ArgumentException("Plan code is required");
            }

            if (seatCount <= 0)
            {
                throw new ArgumentException("Seat count must be positive");
            }

            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                throw new ArgumentException("Payment method is required");
            }

            string normalizedPlanCode = planCode.Trim().ToUpperInvariant();
            string normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();

            var customerRepository = new CustomerRepository();
            var planRepository = new SubscriptionPlanRepository();

            var customer = customerRepository.GetById(customerId);
            var plan = planRepository.GetByCode(normalizedPlanCode);

            if (!customer.IsActive)
            {
                throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
            }

            decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;
            
            CustomerDiscountCalculator customerDiscountCalculator = new CustomerDiscountCalculator(
                [new SegmentDiscount(), new SeatDiscount(), new LoyaltyDiscount(), new PointsDiscount(useLoyaltyPoints)]);

            var (discountAmount, notes) = customerDiscountCalculator.CalculateAll(new CustomerDiscountContext(
                customer.Segment,
                customer.YearsWithCompany,
                customer.LoyaltyPoints,
                baseAmount,
                plan.IsEducationEligible,
                seatCount));

            var amountAfterDiscount = baseAmount - discountAmount;

            var supportPlan = plan.GetPlanSupport(includePremiumSupport);
            decimal supportFee = supportPlan.Item1;
            notes += supportPlan.Item2;
            
            PaymentFeeCalculator feeCalculator = new PaymentFeeCalculator();
            var fee = feeCalculator.CalculateTotalAmount(normalizedPaymentMethod, amountAfterDiscount + supportFee);
            
            decimal paymentFee = fee.Amount;
            notes += fee.Note;

            decimal taxRate = CountryTaxRate.GetTaxRate(customer.Country);

            decimal taxBase = amountAfterDiscount + supportFee + paymentFee;
            decimal taxAmount = taxBase * taxRate;
            decimal finalAmount = taxBase + taxAmount;

            if (finalAmount < 500m)
            {
                finalAmount = 500m;
                notes += "minimum invoice amount applied; ";
            }

            var invoice = new RenewalInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customerId}-{normalizedPlanCode}",
                CustomerName = customer.FullName,
                PlanCode = normalizedPlanCode,
                PaymentMethod = normalizedPaymentMethod,
                SeatCount = seatCount,
                BaseAmount = Math.Round(baseAmount, 2, MidpointRounding.AwayFromZero),
                DiscountAmount = Math.Round(discountAmount, 2, MidpointRounding.AwayFromZero),
                SupportFee = Math.Round(supportFee, 2, MidpointRounding.AwayFromZero),
                PaymentFee = Math.Round(paymentFee, 2, MidpointRounding.AwayFromZero),
                TaxAmount = Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
                FinalAmount = Math.Round(finalAmount, 2, MidpointRounding.AwayFromZero),
                Notes = notes.Trim(),
                GeneratedAt = DateTime.UtcNow
            };

            LegacyBillingGateway.SaveInvoice(invoice);

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                string subject = "Subscription renewal invoice";
                string body =
                    $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                    $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

                LegacyBillingGateway.SendEmail(customer.Email, subject, body);
            }

            return invoice;
        }
    }
}
