using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int BookingReservationId { get; set; }

    public int RoomId { get; set; }

    public decimal RoomPrice { get; set; }

    public decimal ActualPrice { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual BookingReservation BookingReservation { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
