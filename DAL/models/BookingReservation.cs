using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class BookingReservation
{
    public int BookingReservationId { get; set; }

    public int CustomerId { get; set; }

    public DateTime BookingDate { get; set; }

    public DateOnly CheckinDate { get; set; }

    public DateOnly CheckoutDate { get; set; }

    public decimal TotalPrice { get; set; }

    public int BookingStatus { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;
}
