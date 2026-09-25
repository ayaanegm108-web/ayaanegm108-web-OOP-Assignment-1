using SingleBuilderNs = Part3_BuilderPattern.SingleBuilder;
using ComposedNs = Part3_BuilderPattern.Composed;
namespace Part3_BuilderPattern;

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("# Task 3.2 — single fluent builder \n");

            var invoiceA = new SingleBuilderNs.InvoiceBuilder()
                .WithInvoiceId("INV-1001")
                .WithCustomer("Mona Ali", "mona@example.com", "010-1111-2222")
                .WithBillingAddress("12 Tahrir St", "Cairo", "Cairo", "11511", "Egypt")
                .WithShippingAddress("45 Corniche Rd", "Alexandria", "Alexandria", "21500", "Egypt")
                .WithOrderDate(new DateTime(2026, 9, 20))
                .WithPayment("Credit Card", "EGP")
                .WithAmounts(subTotal: 1500m, totalAmount: 1400m, discountAmount: 150m, taxAmount: 50m)
                .Build();

            invoiceA.Print();

            Console.WriteLine("\n# Task 3.3 — composed builders (AddressBuilder + OrderBuilder) \n");

            var billing = new ComposedNs.AddressBuilder()
                .WithStreet("12 Tahrir St")
                .WithCity("Cairo")
                .WithState("Cairo")
                .WithZipCode("11511")
                .WithCountry("Egypt")
                .Build();

           
            var shipping = new ComposedNs.AddressBuilder()
                .WithStreet("45 Corniche Rd")
                .WithCity("Alexandria")
                .WithState("Alexandria")
                .WithZipCode("21500")
                .WithCountry("Egypt")
                .Build();

            var order = new ComposedNs.OrderBuilder()
                .WithOrderDate(new DateTime(2026, 9, 20))
                .WithPayment("Credit Card", "EGP")
                .WithSubTotal(1500m)
                .WithDiscount(150m)
                .WithTax(50m)
                .WithTotal(1400m)
                .Build();

            var invoiceB = new ComposedNs.InvoiceBuilder()
                .WithInvoiceId("INV-1002")
                .WithCustomer("Omar Hassan", "omar@example.com", "010-3333-4444")
                .WithBillingAddress(billing)
                .WithShippingAddress(shipping)
                .WithOrderInfo(order)
                .Build();

            invoiceB.Print();

            Console.WriteLine("\n# Missing a mandatory field throws immediately \n");
            try
            {
                new SingleBuilderNs.InvoiceBuilder()
                    .WithInvoiceId("INV-9999")
                    .Build();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expected failure: {ex.Message}");
            }
        }
    }


