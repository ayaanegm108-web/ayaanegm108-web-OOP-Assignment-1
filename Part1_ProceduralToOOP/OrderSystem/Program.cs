namespace OrderSystem
{
    internal class Program
    {
        private static void PrintMenu()
        {
            Console.WriteLine("\n---------- MENU ----------");
            Console.WriteLine("1) Print customers");
            Console.WriteLine("2) Print products");
            Console.WriteLine("3) Print all orders");
            Console.WriteLine("4) Print one order by id");
            Console.WriteLine("5) Create order");
            Console.WriteLine("6) Add line to order");
            Console.WriteLine("7) Mark order paid");
            Console.WriteLine("8) Show paid sales total");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");
        }

        private static int ReadInt()
        {
            string input = Console.ReadLine()!;
            bool IsValid = int.TryParse(input , out int number);
            while (!IsValid)
                Console.Write("Please enter a whole number: ");
            return number;
        }

        private static void RunInteractiveMenu(System system)
        {
            
            int choice = -1;
            while (choice != 0)
            {
                PrintMenu();
                choice = ReadInt();

                switch (choice)
                {
                    case 1:
                        system.PrintCustomers();
                        break;
                    case 2:
                        system.PrintProducts();
                        break;
                    case 3:
                        system.PrintAllOrders();
                        break;
                    case 4:
                        Console.Write("Order id: ");
                        system.PrintOrder(ReadInt());
                        break;
                    case 5:
                        {
                            Console.Write("Order id: ");
                            int orderId = ReadInt();
                            Console.Write("Customer id: ");
                            int customerId = ReadInt();
                            Console.Write("Date (YYYY-MM-DD): ");
                            string date = Console.ReadLine() ?? "";
                            system.CreateOrder(orderId, customerId, date);
                            break;
                        }
                    case 6:
                        {
                            Console.Write("Order id: ");
                            int orderId = ReadInt();
                            Console.Write("Product id: ");
                            int productId = ReadInt();
                            Console.Write("Quantity: ");
                            int quantity = ReadInt();
                            system.AddLineToOrder(orderId, productId, quantity);
                            break;
                        }
                    case 7:
                        Console.Write("Order id: ");
                        system.MarkOrderPaid(ReadInt());
                        break;
                    case 8:
                        Console.WriteLine($"Paid sales total: {system.TotalSalesPaidOnly():0.00}");
                        break;
                    case 0:
                        Console.WriteLine("Bye.");
                        break;
                    default:
                        Console.WriteLine("Unknown choice.");
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            System system = new System();
            RunInteractiveMenu(system);
        }
    }
}
