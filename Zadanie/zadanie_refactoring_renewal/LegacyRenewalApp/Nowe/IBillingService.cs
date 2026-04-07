namespace LegacyRenewalApp;

public interface IBillingService
{
    // LegacyBillingGateway.SaveInvoice(invoice);
    void SaveInvoice(RenewalInvoice invoice);
    void SentMessage(string recipent, string subject, string body);
    
}