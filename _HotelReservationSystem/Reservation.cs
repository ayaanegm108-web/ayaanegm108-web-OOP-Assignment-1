namespace Part2_HotelReservationSystem;
public class Reservation
{
    public string ReservationId { get; }
    public DateTime CheckInDate { get; }
    public DateTime CheckOutDate { get; }
    public Room Room { get; }
    public ReservationStatus Status { get; private set; }

    internal Reservation(string reservationId, Room room, DateTime checkIn, DateTime checkOut)
    {
        if (checkOut <= checkIn)
            throw new ArgumentException("Check-out date must be strictly after check-in date.");
        if (room.IsUnderMaintenance)
            throw new InvalidOperationException($"Room {room.RoomNumber} is under maintenance and cannot be booked.");
        if (!room.IsAvailable(checkIn, checkOut))
            throw new InvalidOperationException(
                $"Room {room.RoomNumber} already has an overlapping active reservation for that date range.");

        ReservationId = reservationId;
        Room = room;
        CheckInDate = checkIn;
        CheckOutDate = checkOut;
        Status = ReservationStatus.Pending;

        room.RegisterReservation(this);
    }

    public int Nights => (CheckOutDate - CheckInDate).Days;

    public decimal TotalCost => Nights * Room.NightlyRate;

    public void Confirm()
    {
        if (Status != ReservationStatus.Pending)
            throw new InvalidOperationException($"Cannot confirm a reservation in status {Status}.");
        Status = ReservationStatus.Confirmed;
    }

    public void CheckIn()
    {
        if (Status != ReservationStatus.Confirmed)
            throw new InvalidOperationException("A reservation can only be checked in once it is Confirmed.");
        Status = ReservationStatus.CheckedIn;
    }

    public void CheckOut()
    {
        if (Status != ReservationStatus.CheckedIn)
            throw new InvalidOperationException($"Cannot check out a reservation in status {Status}.");
        Status = ReservationStatus.CheckedOut;
    }

    public void Cancel()
    {
        if (Status is not (ReservationStatus.Pending or ReservationStatus.Confirmed))
            throw new InvalidOperationException($"Cannot cancel a reservation in status {Status}.");
        Status = ReservationStatus.Cancelled;
    }

    public override string ToString() =>
        $"Reservation {ReservationId}  Room {Room.RoomNumber}  " +
        $"{CheckInDate:yyyy-MM-dd} -> {CheckOutDate:yyyy-MM-dd}  {Status}  total={TotalCost:0.00}";
}
