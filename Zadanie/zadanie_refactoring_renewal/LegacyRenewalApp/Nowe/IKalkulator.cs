using System;

namespace LegacyRenewalApp;

public interface IKalkulator
{
    (decimal Amount, string Notes) CalculateDiscount(Customer customer, SubscriptionPlan plan, decimal baseAmount);
    (decimal Amount, string Notes) CalculateSeats(int seatCount, decimal baseAmount);
    (decimal Amount, string Notes) CalculatePaymentMethod(string normalizedPlanCode, bool includePremiumSupport);
    (decimal Amount, string Notes) CalculatePaymentFee(string normalizedPaymentMethod, decimal amountSoFar);
    (decimal Amount, decimal Rate) CalculateTax(string country, decimal taxBase);
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
            case ("Silver", _):
                discountAmount += baseAmount * 0.05m;
                notes += "silver discount; ";
                break;
            case ("Gold", _):
                discountAmount += baseAmount * 0.10m;
                notes += "gold discount; ";
                break;
            case ("Platinum", _):
                discountAmount += baseAmount * 0.15m;
                notes += "platinum discount; ";
                break;
            case ("Education", true):
                discountAmount += baseAmount * 0.20m;
                notes += "education discount; ";
                break;

        }

        return (discountAmount, notes);
    }

    public (decimal Amount, string Notes) CalculateSeats(int seatCount, decimal baseAmount)
    {

        decimal discountAmount = 0m;
        string notes = string.Empty;

        switch (seatCount)
        {
            case >= 50:
                discountAmount += baseAmount * 0.12m;
                notes += "large team discount; ";
                break;
            case >= 20:
                discountAmount += baseAmount * 0.08m;
                notes += "medium team discount; ";
                break;
            case >= 10:
                discountAmount += baseAmount * 0.04m;
                notes += "small team discount; ";
                break;

        }

        return (discountAmount, notes);
    }

    public (decimal Amount, string Notes) CalculatePaymentMethod(string normalizedPlanCode, bool includePremiumSupport)
    {
        decimal supportFee = 0m;
        string notes = string.Empty;
        if (includePremiumSupport)
        {
            switch (normalizedPlanCode)
            {
                case "SUPPORT":
                    supportFee = 250m;
                    break;
                case "PRO":
                    supportFee = 400m;
                    break;
                case "ENTERPRISE":
                    supportFee = 700m;
                    break;
            }

            notes += "premium support included; ";

        }

        return (supportFee, notes);
    }
    
    public (decimal Amount, string Notes) CalculatePaymentFee(string normalizedPaymentMethod, decimal amountSoFar)
    {
        decimal paymentFee = 0m;
        string notes = string.Empty;

        switch (normalizedPaymentMethod)
        {
            case "CARD":
                paymentFee = amountSoFar * 0.02m;
                notes = "card payment fee; ";
                break;
            case "BANK_TRANSFER":
                paymentFee = amountSoFar * 0.01m;
                notes = "bank transfer fee; ";
                break;
            case "PAYPAL":
                paymentFee = amountSoFar * 0.035m;
                notes = "paypal fee; ";
                break;
            case "INVOICE":
                paymentFee = 0m;
                notes = "invoice payment; ";
                break;
            default:
                throw new ArgumentException("Unsupported payment method");
        }

        return (paymentFee, notes);
    }
    
    public (decimal Amount, decimal Rate) CalculateTax(string country, decimal taxBase)
    {
        decimal taxRate = country switch
        {
            "Poland"         => 0.23m,
            "Germany"        => 0.19m,
            "Czech Republic" => 0.21m,
            "Norway"         => 0.25m,
            _                => 0.00m 
        };

        decimal taxAmount = taxBase * taxRate;
        return (taxAmount, taxRate);
    }
}
