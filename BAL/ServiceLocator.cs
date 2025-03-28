using BAL.Interfaces;
using BAL.Services;
using DAL.Models;
using DAL.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BAL
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        private static readonly object _lock = new object();
        
        // Database context
        private readonly FUMiniHotelManagementContext _dbContext;
        
        // Repositories
        private readonly UserRepository _userRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly RoomRepository _roomRepository;
        private readonly RoomTypeRepository _roomTypeRepository;
        private readonly BookingReservationRepository _bookingRepository;
        private readonly BookingDetailRepository _bookingDetailRepository;
        
        // Services
        private readonly IAuthService _authService;
        private readonly ICustomerService _customerService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly IReportService _reportService;

        private ServiceLocator()
        {
            // Initialize database context
            _dbContext = new FUMiniHotelManagementContext();
            
            // Initialize repositories
            _userRepository = new UserRepository(_dbContext);
            _customerRepository = new CustomerRepository(_dbContext);
            _roomRepository = new RoomRepository(_dbContext);
            _roomTypeRepository = new RoomTypeRepository(_dbContext);
            _bookingRepository = new BookingReservationRepository(_dbContext);
            _bookingDetailRepository = new BookingDetailRepository(_dbContext);
            
            // Initialize services
            _authService = new AuthService(_userRepository);
            _customerService = new CustomerService(_customerRepository);
            _roomService = new RoomService(_roomRepository, _roomTypeRepository, _bookingDetailRepository);
            _bookingService = new BookingService(_bookingRepository, _bookingDetailRepository, _roomRepository);
            _reportService = new ReportService(_bookingRepository, _bookingDetailRepository, _roomRepository, _dbContext);
        }

        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServiceLocator();
                        }
                    }
                }
                return _instance;
            }
        }

        // Service accessors
        public IAuthService AuthService => _authService;
        public ICustomerService CustomerService => _customerService;
        public IRoomService RoomService => _roomService;
        public IBookingService BookingService => _bookingService;
        public IReportService ReportService => _reportService;
    }
}