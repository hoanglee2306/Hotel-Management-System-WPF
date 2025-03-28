using BAL;
using DAL.Models;
using hoanglee.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace hoanglee.ViewModels
{
    public class RoomManagementViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _rooms;
        private Room _selectedRoom;
        private string _searchText;
        private bool _isDialogOpen;
        private Room _dialogRoom;
        private ObservableCollection<RoomType> _roomTypes;

        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set => SetProperty(ref _rooms, value);
        }

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                if (SetProperty(ref _selectedRoom, value))
                {
                    // Update commands that depend on selection
                    ((RelayCommand)EditRoomCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)DeleteRoomCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    SearchRooms();
                }
            }
        }

        public bool IsDialogOpen
        {
            get => _isDialogOpen;
            set => SetProperty(ref _isDialogOpen, value);
        }

        public Room DialogRoom
        {
            get => _dialogRoom;
            set => SetProperty(ref _dialogRoom, value);
        }

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set => SetProperty(ref _roomTypes, value);
        }

        public ICommand AddRoomCommand { get; }
        public ICommand EditRoomCommand { get; }
        public ICommand DeleteRoomCommand { get; }
        public ICommand SaveRoomCommand { get; }
        public ICommand CancelDialogCommand { get; }

        public RoomManagementViewModel()
        {
            AddRoomCommand = new RelayCommand(ExecuteAddRoom);
            EditRoomCommand = new RelayCommand(ExecuteEditRoom, CanExecuteRoomAction);
            DeleteRoomCommand = new RelayCommand(ExecuteDeleteRoom, CanExecuteRoomAction);
            SaveRoomCommand = new RelayCommand(ExecuteSaveRoom, CanExecuteSaveRoom);
            CancelDialogCommand = new RelayCommand(ExecuteCancelDialog);

            LoadRooms();
            LoadRoomTypes();
        }

        private void LoadRooms()
        {
            try
            {
                var roomService = ServiceLocator.Instance.RoomService;
                var roomList = roomService.GetAllRooms().ToList();
                Rooms = new ObservableCollection<Room>(roomList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rooms: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadRoomTypes()
        {
            try
            {
                var roomService = ServiceLocator.Instance.RoomService;
                var roomTypeList = roomService.GetAllRoomTypes().ToList();
                RoomTypes = new ObservableCollection<RoomType>(roomTypeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room types: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchRooms()
        {
            try
            {
                var roomService = ServiceLocator.Instance.RoomService;
                var roomList = roomService.SearchRooms(SearchText).ToList();
                Rooms = new ObservableCollection<Room>(roomList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching rooms: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteAddRoom(object parameter)
        {
            DialogRoom = new Room
            {
                RoomStatus = 1 // Active by default
            };
            IsDialogOpen = true;
        }

        private void ExecuteEditRoom(object parameter)
        {
            if (SelectedRoom != null)
            {
                // Create a clone of the selected room to avoid modifying the original until save
                DialogRoom = new Room
                {
                    RoomId = SelectedRoom.RoomId,
                    RoomNumber = SelectedRoom.RoomNumber,
                    RoomDescription = SelectedRoom.RoomDescription,
                    RoomMaxCapacity = SelectedRoom.RoomMaxCapacity,
                    RoomStatus = SelectedRoom.RoomStatus,
                    RoomPricePerDate = SelectedRoom.RoomPricePerDate,
                    RoomTypeId = SelectedRoom.RoomTypeId
                };
                IsDialogOpen = true;
            }
        }

        private void ExecuteDeleteRoom(object parameter)
        {
            if (SelectedRoom != null)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this room? If the room is used in bookings, it will be deactivated instead.",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var roomService = ServiceLocator.Instance.RoomService;
                        bool success = roomService.DeleteRoom(SelectedRoom.RoomId);

                        if (success)
                        {
                            // Reload rooms to reflect changes
                            LoadRooms();
                            MessageBox.Show("Room deleted or deactivated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting room: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private bool CanExecuteRoomAction(object parameter)
        {
            return SelectedRoom != null;
        }

        private void ExecuteSaveRoom(object parameter)
        {
            if (DialogRoom == null) return;

            // Validate room data
            if (string.IsNullOrWhiteSpace(DialogRoom.RoomNumber))
            {
                MessageBox.Show("Room number is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DialogRoom.RoomMaxCapacity <= 0)
            {
                MessageBox.Show("Room capacity must be greater than zero.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DialogRoom.RoomPricePerDate <= 0)
            {
                MessageBox.Show("Room price must be greater than zero.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DialogRoom.RoomTypeId <= 0)
            {
                MessageBox.Show("Please select a room type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var roomService = ServiceLocator.Instance.RoomService;

                // Check if this is an add or update
                if (DialogRoom.RoomId == 0)
                {
                    // Add new room
                    roomService.AddRoom(DialogRoom);
                    MessageBox.Show("Room added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Update existing room
                    roomService.UpdateRoom(DialogRoom);
                    MessageBox.Show("Room updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Reload rooms and close dialog
                LoadRooms();
                IsDialogOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving room: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteSaveRoom(object parameter)
        {
            return DialogRoom != null &&
                   !string.IsNullOrWhiteSpace(DialogRoom.RoomNumber) &&
                   DialogRoom.RoomMaxCapacity > 0 &&
                   DialogRoom.RoomPricePerDate > 0 &&
                   DialogRoom.RoomTypeId > 0;
        }

        private void ExecuteCancelDialog(object parameter)
        {
            IsDialogOpen = false;
        }
    }
}