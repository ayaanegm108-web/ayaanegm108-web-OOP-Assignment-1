namespace Part3_BuilderPattern;

public class Invoice
{
   
    public string InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string CustomerPhone { get; }

    //============================================================================
    public string BillingStreet { get; }
    public string BillingCity { get; }
    public string BillingState { get; }
    public string BillingZipCode { get; }
    public string BillingCountry { get; }

    // ====================================================================================
    public string ShippingStreet { get; }
    public string ShippingCity { get; }
    public string ShippingState { get; }
    public string ShippingZipCode { get; }
    public string ShippingCountry { get; }

    //=========================================================================================
    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }

    internal Invoice(
        string invoiceId, string customerName, string customerEmail, string customerPhone,
        string billingStreet, string billingCity, string billingState, string billingZipCode, string billingCountry,
        string shippingStreet, string shippingCity, string shippingState, string shippingZipCode, string shippingCountry,
        DateTime orderDate, string paymentMethod, string currency,
        decimal subTotal, decimal discountAmount, decimal taxAmount, decimal totalAmount)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;

        BillingStreet = billingStreet;
        BillingCity = billingCity;
        BillingState = billingState;
        BillingZipCode = billingZipCode;
        BillingCountry = billingCountry;

        ShippingStreet = shippingStreet;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingZipCode = shippingZipCode;
        ShippingCountry = shippingCountry;

        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        TotalAmount = totalAmount;
    }

    public void Print()
    {
        Console.WriteLine($"Invoice {InvoiceId} — {CustomerName} <{CustomerEmail}> {CustomerPhone}");
        Console.WriteLine($"  Bill to:  {BillingStreet}, {BillingCity}, {BillingState} {BillingZipCode}, {BillingCountry}");
        Console.WriteLine($"  Ship to:  {ShippingStreet}, {ShippingCity}, {ShippingState} {ShippingZipCode}, {ShippingCountry}");
        Console.WriteLine($"  {OrderDate:yyyy-MM-dd}  {PaymentMethod}  {Currency}");
        Console.WriteLine($"  SubTotal={SubTotal:0.00}  Discount={DiscountAmount:0.00}  Tax={TaxAmount:0.00}  Total={TotalAmount:0.00}");
    }
}
