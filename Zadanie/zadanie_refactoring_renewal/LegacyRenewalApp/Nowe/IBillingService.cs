namespace LegacyRenewalApp;

public interface IBillingService {
    void Save(RenewalInvoice invoice);
}

public class BillingService : IBillingService {
    public void Save(RenewalInvoice invoice) {
        LegacyBillingGateway.SaveInvoice(invoice);
    }
}