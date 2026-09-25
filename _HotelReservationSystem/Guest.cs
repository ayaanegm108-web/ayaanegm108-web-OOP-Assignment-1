namespace Part2_HotelReservationSystem;


public class Guest
{
    private readonly List<Reservation> _reservations = new();
    private int _nextReservationSequence = 1;

    public string GuestId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }

    public IReadOnlyList<Reservation> Reservations => _reservations;

    public Guest(string guestId, string fullName, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(guestId))
            throw new ArgumentException("Guest id is required.", nameof(guestId));
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.", nameof(fullName));
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

        GuestId = guestId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

   
    public Reservation MakeReservation(Room room, DateTime checkIn, DateTime checkOut)
    {
        string reservationId = $"{GuestId}-R{_nextReservationSequence++}";
        var reservation = new Reservation(reservationId, room, checkIn, checkOut);
        _reservations.Add(reservation);
        return reservation;
    }

    public override string ToString() => $"Guest {GuestId}  {FullName}  {PhoneNumber}";
}
