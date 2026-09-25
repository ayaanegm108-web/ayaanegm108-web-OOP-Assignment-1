namespace Part3_BuilderPattern.Composed;
public class OrderBuilder
{
    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal _discountAmount = 0m;
    private decimal _taxAmount = 0m;
    private decimal? _totalAmount;

    public OrderBuilder WithOrderDate(DateTime orderDate) { _orderDate = orderDate; return this; }
    public OrderBuilder WithPayment(string paymentMethod, string currency) { _paymentMethod = paymentMethod; _currency = currency; return this; }
    public OrderBuilder WithSubTotal(decimal subTotal) { _subTotal = subTotal; return this; }
    public OrderBuilder WithDiscount(decimal discountAmount) { _discountAmount = discountAmount; return this; }
    public OrderBuilder WithTax(decimal taxAmount) { _taxAmount = taxAmount; return this; }
    public OrderBuilder WithTotal(decimal totalAmount) { _totalAmount = totalAmount; return this; }

    public OrderInfo Build()
    {
        var missing = new List<string>();
        if (_orderDate is null) missing.Add("OrderDate");
        if (string.IsNullOrWhiteSpace(_paymentMethod)) missing.Add("PaymentMethod");
        if (string.IsNullOrWhiteSpace(_currency)) missing.Add("Currency");
        if (_subTotal is null) missing.Add("SubTotal");
        if (_totalAmount is null) missing.Add("TotalAmount");

        if (missing.Count > 0)
            throw new InvalidOperationException(
                $"Cannot build OrderInfo — missing required field(s): {string.Join(", ", missing)}");

        return new OrderInfo(_orderDate!.Value, _paymentMethod!, _currency!,
            _subTotal!.Value, _discountAmount, _taxAmount, _totalAmount!.Value);
    }
}
