using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class RoomType
{
    public int RoomTypeId { get; set; }

    public string RoomTypeName { get; set; } = null!;

    public string? TypeDescription { get; set; }

    public string? TypeNote { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
