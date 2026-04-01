namespace LegacyRenewalApp
{
    public class SubscriptionPlan
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal MonthlyPricePerSeat { get; set; }
        public decimal SetupFee { get; set; }
        public bool IsEducationEligible { get; set; }


        public decimal getPlanCodeSupportFee(bool includePremiumSupport)
        {
            if (includePremiumSupport)
            {
                if (Code == "START")
                {
                    return 250m;
                }
                else if (Code == "PRO")
                {
                    return 400m;
                }
                else if (Code == "ENTERPRISE")
                {
                    return 700m;
                }
            }
            return 0m;
        }

        public string getPlanCodeSupprtFeeNote(bool includePremiumSupport)
        {
            if (includePremiumSupport)
                return "premium support included; ";
            return string.Empty;
        }
    }
}
