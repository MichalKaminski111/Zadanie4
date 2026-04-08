using System.Collections.Generic;

namespace LegacyRenewalApp
{
    public class SubscriptionPlan
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal MonthlyPricePerSeat { get; set; }
        public decimal SetupFee { get; set; }
        public bool IsEducationEligible { get; set; }


        private readonly Dictionary<string, decimal> PlansSupportFee = new()
        {
            { "START", 250m},
            { "PRO", 400m},
            { "ENTERPRISE", 700m},
        };
        
        public (decimal, string) GetPlanSupport(bool includePremiumSupport)
        {
            string note = "";
            decimal fee = 0m;
            if (includePremiumSupport)
                note += "premium support included; ";
            if (!PlansSupportFee.TryGetValue(Code, out fee))
                note += "unknown plan code for support fee; ";

            return (fee, note);
        }
    }
}
