namespace Part2_HotelReservationSystem;
public class Room
{
    private readonly List<Reservation> _reservations = new();

    public string RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }

    public IReadOnlyList<Reservation> Reservations => _reservations;

    public Room(string roomNumber, RoomType roomType, decimal nightlyRate)
    {
        if (string.IsNullOrWhiteSpace(roomNumber))
            throw new ArgumentException("Room number is required.", nameof(roomNumber));
        if (nightlyRate <= 0)
            throw new ArgumentException("Nightly rate must be positive.", nameof(nightlyRate));

        RoomNumber = roomNumber;
        RoomType = roomType;
        NightlyRate = nightlyRate;
    }

    
    public void ChangeRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentException("Nightly rate must be positive.", nameof(newRate));
        NightlyRate = newRate;
    }

    public void StartMaintenance() => IsUnderMaintenance = true;

    public void EndMaintenance() => IsUnderMaintenance = false;
    public bool IsAvailable(DateTime checkIn, DateTime checkOut)
    {
        if (IsUnderMaintenance)
            return false;

        return !_reservations.Any(r =>
            IsActive(r.Status) &&
            checkIn < r.CheckOutDate && r.CheckInDate < checkOut);
    }

    private static bool IsActive(ReservationStatus status) =>
        status is ReservationStatus.Pending or ReservationStatus.Confirmed or ReservationStatus.CheckedIn;
    internal void RegisterReservation(Reservation reservation) => _reservations.Add(reservation);

    public override string ToString() =>
        $"Room {RoomNumber} ({RoomType})  rate={NightlyRate:0.00}  maintenance={(IsUnderMaintenance ? "yes" : "no")}";
}
