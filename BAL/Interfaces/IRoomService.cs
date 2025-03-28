using DAL.Models;

namespace BAL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAllRooms();
        IEnumerable<Room> GetActiveRooms();
        Room GetRoomById(int id);
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        bool DeleteRoom(int id);
        void UpdateRoomStatus(int roomId, int status);
        IEnumerable<Room> SearchRooms(string searchTerm);
        IEnumerable<Room> GetAvailableRooms(DateOnly startDate, DateOnly endDate);
        IEnumerable<RoomType> GetAllRoomTypes();
    }
}