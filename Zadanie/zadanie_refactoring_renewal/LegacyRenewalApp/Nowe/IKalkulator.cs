namespace LegacyRenewalApp;

public interface IKalkulator
{
    (decimal Amount, string Notes) CalculateDiscount(Customer customer, SubscriptionPlan plan, decimal baseAmount);
}

public class Kalkulator : IKalkulator
{
    public (decimal Amount, string Notes) CalculateDiscount(Customer customer, SubscriptionPlan plan,
        decimal baseAmount)
    {
        decimal discountAmount = 0m;
        string notes = string.Empty;
        
        switch (customer.Segment, plan.IsEducationEligible)
        {
            case ("Silver",_):
                discountAmount += baseAmount * 0.05m;
                notes += "silver discount; ";
                break;
            case ("Gold",_):
                discountAmount += baseAmount * 0.10m;
                notes += "gold discount; ";
                break;
            case ("Platinum",_):
                discountAmount += baseAmount * 0.15m;
                notes += "platinum discount; ";
                break;
            case ("Education", true):
                discountAmount += baseAmount * 0.20m;
                notes += "education discount; ";
                break;
                
        }
        return  (discountAmount, notes);
    }
}