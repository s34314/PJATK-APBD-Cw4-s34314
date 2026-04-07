namespace LegacyRenewalApp;

public class IBillingServiceAdapter : IBillingService
{
    public void SaveInvoice(RenewalInvoice invoice)
    {
        LegacyBillingGateway.SaveInvoice(invoice);
    }

    public void SentMessage(string recipient, string subject, string body)
    {
        LegacyBillingGateway.SendEmail(recipient, subject, body);
    }
}