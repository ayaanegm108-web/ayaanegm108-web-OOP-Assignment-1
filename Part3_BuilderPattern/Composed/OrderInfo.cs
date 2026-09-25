namespace Part3_BuilderPattern.Composed;
public class OrderInfo
{
    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }

    public OrderInfo(DateTime orderDate, string paymentMethod, string currency,
        decimal subTotal, decimal discountAmount, decimal taxAmount, decimal totalAmount)
    {
        if (string.IsNullOrWhiteSpace(paymentMethod))
            throw new ArgumentException("Payment method is required.", nameof(paymentMethod));
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.", nameof(currency));

        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        TotalAmount = totalAmount;
    }
}
