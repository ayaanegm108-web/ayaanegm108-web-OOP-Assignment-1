using Part2_HotelReservationSystem;

namespace _HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var room101 = new Room("101", RoomType.Single, 50.00m);
            var room202 = new Room("202", RoomType.Suite, 180.00m);

            var mona = new Guest("G1", "Mona Ali", "010-1111-2222");
            var omar = new Guest("G2", "Omar Hassan", "010-3333-4444");
            var res1 = mona.MakeReservation(room101, new DateTime(2026, 10, 1), new DateTime(2026, 10, 4));
            Console.WriteLine($"Created: {res1}");

            res1.Confirm();
            res1.CheckIn();
            Console.WriteLine($"After confirm + check-in: {res1}");
            try
            {
                omar.MakeReservation(room101, new DateTime(2026, 10, 2), new DateTime(2026, 10, 3));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nExpected rejection (overlap): {ex.Message}");

            }
            try
            {
                res1.Cancel(); 
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expected rejection (bad transition): {ex.Message}");
            }
            room202.StartMaintenance();
            try
            {
                omar.MakeReservation(room202, new DateTime(2026, 11, 1), new DateTime(2026, 11, 2));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expected rejection (maintenance): {ex.Message}");
            }
            room202.EndMaintenance();
            var res2 = omar.MakeReservation(room202, new DateTime(2026, 11, 1), new DateTime(2026, 11, 4));
            res2.Confirm();
            Console.WriteLine($"\nCreated after maintenance ended: {res2}");
            Console.WriteLine($"Total cost ({res2.Nights} nights @ {room202.NightlyRate:0.00}): {res2.TotalCost:0.00}");
            try
            {
                room202.ChangeRate(0m);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nExpected rejection (bad rate): {ex.Message}");
            }

            Console.WriteLine("\n=== Mona's reservation history ===");
            foreach (var r in mona.Reservations)
                Console.WriteLine(r);

            Console.WriteLine("\n=== Omar's reservation history ===");
            foreach (var r in omar.Reservations)
                Console.WriteLine(r);
        }
    }
}
