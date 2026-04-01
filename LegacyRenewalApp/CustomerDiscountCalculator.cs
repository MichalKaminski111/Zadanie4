namespace LegacyRenewalApp;

public class CustomerDiscountCalculator
{
    private string Segment;
    private int YearsWithCompany;
    private int baseAmount;

    public CustomerDiscountCalculator(string segment, int yearsWithCompany)
    {
        Segment = segment;
        YearsWithCompany = yearsWithCompany;
    }

    public decimal getALlDiscounts(bool isIsEducationEligible, decimal seatCount)
    {
        decimal discountAmount = 0;
        discountAmount += getSegmentDiscount(isIsEducationEligible);
        discountAmount += getSeatDiscount(seatCount);
        discountAmount += getLoyaltyDiscount();
        
        return discountAmount;
    }

    public string getALlNotes(bool isIsEducationEligible, decimal seatCount)
    {
        string notes = "";
        notes += getSegmentNote(isIsEducationEligible);
        notes += getSeatNote(seatCount);
        notes += getLoyaltyNote();
        
        return notes;
    }
    
    public decimal getSegmentDiscount(bool IsEducationEligible)
        {
            if (Segment == "Silver")
            {
                return baseAmount * 0.05m;
            }
            else if (Segment == "Gold")
            {
                return baseAmount * 0.10m;
            }
            else if (Segment == "Platinum")
            {
                return baseAmount * 0.15m;
            }
            else if (Segment == "Education" && IsEducationEligible)
            {
                return baseAmount * 0.20m;
            }
            
            return 0m;
        }

        public string getSegmentNote(bool IsEducationEligible)
        {
            if (Segment == "Silver")
            {
                return "silver discount; ";
            }
            else if (Segment == "Gold")
            {
                return "gold discount; ";
            }
            else if (Segment == "Platinum")
            {
                return "platinum discount; ";
            }
            else if (Segment == "Education" && IsEducationEligible)
            {
                return "education discount; ";
            }
            
            return string.Empty;
        }

        public decimal getLoyaltyDiscount()
        {
            if (YearsWithCompany >= 5)
            {
                return baseAmount * 0.07m;
            }
            else if (YearsWithCompany >= 2)
            {
                return baseAmount * 0.03m;
            }
            return 0m;
        }

        public string getLoyaltyNote()
        {
            if (YearsWithCompany >= 5)
            {
                return "long-term loyalty discount; ";
            }
            else if (YearsWithCompany >= 2)
            {
                return "basic loyalty discount; ";
            }
            return string.Empty;
        }

        public decimal getSeatDiscount(decimal seatCount)
        {
            if (seatCount >= 50)
            {
                return baseAmount * 0.12m;
            }
            else if (seatCount >= 20)
            {
                return baseAmount * 0.08m;
            }
            else if (seatCount >= 10)
            {
                return baseAmount * 0.04m;
            }
            return 0m;
        }

        public string getSeatNote(decimal seatCount)
        {
            if (seatCount >= 50)
            {
                return "large team discount; ";
            }
            else if (seatCount >= 20)
            {
                return "medium team discount; ";
            }
            else if (seatCount >= 10)
            {
                return "small team discount; ";
            }
            return string.Empty;
        }
}