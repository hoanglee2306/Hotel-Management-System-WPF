using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public string? RoomDescription { get; set; }

    public int RoomMaxCapacity { get; set; }

    public int RoomStatus { get; set; }

    public decimal RoomPricePerDate { get; set; }

    public int RoomTypeId { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual RoomType RoomType { get; set; } = null!;
}
