using Part3_BuilderPattern;

namespace Part3_BuilderPattern.SingleBuilder;
public class InvoiceBuilder
{
    private string? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string? _billingStreet, _billingCity, _billingState, _billingZip, _billingCountry;
    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal? _totalAmount;

    
    private string _customerPhone = "";
    private string? _shippingStreet, _shippingCity, _shippingState, _shippingZip, _shippingCountry;
    private decimal _discountAmount = 0m;
    private decimal _taxAmount = 0m;

    public InvoiceBuilder WithInvoiceId(string invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }
    //=====================================================
    public InvoiceBuilder WithCustomer(string name, string email, string phone = "")
    {
        _customerName = name;
        _customerEmail = email;
        _customerPhone = phone;
        return this;
    }
    //===============================================
    public InvoiceBuilder WithBillingAddress(string street, string city, string state, string zipCode, string country)
    {
        _billingStreet = street;
        _billingCity = city;
        _billingState = state;
        _billingZip = zipCode;
        _billingCountry = country;
        return this;
    }
    //===============================================
    public InvoiceBuilder WithShippingAddress(string street, string city, string state, string zipCode, string country)
    {
        _shippingStreet = street;
        _shippingCity = city;
        _shippingState = state;
        _shippingZip = zipCode;
        _shippingCountry = country;
        return this;
    }
    //======================================================
    public InvoiceBuilder WithOrderDate(DateTime orderDate)
    {
        _orderDate = orderDate;
        return this;
    }
    //=====================================================
    public InvoiceBuilder WithPayment(string paymentMethod, string currency)
    {
        _paymentMethod = paymentMethod;
        _currency = currency;
        return this;
    }
    //=====================================================
    public InvoiceBuilder WithAmounts(decimal subTotal, decimal totalAmount, decimal discountAmount = 0m, decimal taxAmount = 0m)
    {
        _subTotal = subTotal;
        _totalAmount = totalAmount;
        _discountAmount = discountAmount;
        _taxAmount = taxAmount;
        return this;
    }
    //====================================================
    public Invoice Build()
    {
        var missing = new List<string>();
        if (string.IsNullOrWhiteSpace(_invoiceId)) missing.Add("InvoiceId");
        if (string.IsNullOrWhiteSpace(_customerName)) missing.Add(nameof(_customerName));
        if (string.IsNullOrWhiteSpace(_customerEmail)) missing.Add(nameof(_customerEmail));
        if (string.IsNullOrWhiteSpace(_billingStreet)) missing.Add(nameof(_billingStreet));
        if (string.IsNullOrWhiteSpace(_billingCity)) missing.Add(nameof(_billingCity));
        if (string.IsNullOrWhiteSpace(_billingZip)) missing.Add(nameof(_billingZip));
        if (string.IsNullOrWhiteSpace(_billingCountry)) missing.Add(nameof(_billingCountry));
        if (_orderDate is null) missing.Add(nameof(_orderDate));
        if (string.IsNullOrWhiteSpace(_paymentMethod)) missing.Add(nameof(_paymentMethod));
        if (string.IsNullOrWhiteSpace(_currency)) missing.Add(nameof(_currency));
        if (_subTotal is null) missing.Add(nameof(_subTotal));
        if (_totalAmount is null) missing.Add(nameof(_totalAmount));

        if (missing.Count > 0)
            throw new InvalidOperationException(
                $"Cannot build Invoice — missing required field(s): {string.Join(", ", missing)}");

        return new Invoice(
            _invoiceId!, _customerName!, _customerEmail!, _customerPhone,
            _billingStreet!, _billingCity!, _billingState ?? "", _billingZip!, _billingCountry!,
            _shippingStreet ?? _billingStreet!, _shippingCity ?? _billingCity!,
            _shippingState ?? _billingState ?? "", _shippingZip ?? _billingZip!,
            _shippingCountry ?? _billingCountry!,
            _orderDate!.Value, _paymentMethod!, _currency!,
            _subTotal!.Value, _discountAmount, _taxAmount, _totalAmount!.Value);
    }
}
