namespace Part3_BuilderPattern.Composed;
public class InvoiceBuilder
{
    private string? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string _customerPhone = "";
    private Address? _billingAddress;
    private Address? _shippingAddress;
    private OrderInfo? _orderInfo;

    public InvoiceBuilder WithInvoiceId(string invoiceId) { _invoiceId = invoiceId; return this; }

    public InvoiceBuilder WithCustomer(string name, string email, string phone = "")
    {
        _customerName = name;
        _customerEmail = email;
        _customerPhone = phone;
        return this;
    }

    public InvoiceBuilder WithBillingAddress(Address address) { _billingAddress = address; return this; }
    public InvoiceBuilder WithShippingAddress(Address address) { _shippingAddress = address; return this; }

    public InvoiceBuilder WithOrderInfo(OrderInfo orderInfo) { _orderInfo = orderInfo; return this; }

    public Part3_BuilderPattern.Invoice Build()
    {
        var missing = new List<string>();
        if (string.IsNullOrWhiteSpace(_invoiceId)) missing.Add("InvoiceId");
        if (string.IsNullOrWhiteSpace(_customerName)) missing.Add("CustomerName");
        if (string.IsNullOrWhiteSpace(_customerEmail)) missing.Add("CustomerEmail");
        if (_billingAddress is null) missing.Add("BillingAddress");
        if (_orderInfo is null) missing.Add("OrderInfo");

        if (missing.Count > 0)
            throw new InvalidOperationException(
                $"Cannot build Invoice — missing required field(s): {string.Join(", ", missing)}");

        var shipping = _shippingAddress ?? _billingAddress!;

        return new Part3_BuilderPattern.Invoice(
            _invoiceId!, _customerName!, _customerEmail!, _customerPhone,
            _billingAddress!.Street, _billingAddress.City, _billingAddress.State, _billingAddress.ZipCode, _billingAddress.Country,
            shipping.Street, shipping.City, shipping.State, shipping.ZipCode, shipping.Country,
            _orderInfo!.OrderDate, _orderInfo.PaymentMethod, _orderInfo.Currency,
            _orderInfo.SubTotal, _orderInfo.DiscountAmount, _orderInfo.TaxAmount, _orderInfo.TotalAmount);
    }
}
