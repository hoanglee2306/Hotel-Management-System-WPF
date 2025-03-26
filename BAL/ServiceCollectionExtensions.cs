using System;
using Microsoft.Extensions.DependencyInjection;
using DAL.Repository;
using BAL.Services;
using DAL.Models;

namespace BAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<CustomerRepository>();
            services.AddScoped<RoomRepository>();
            services.AddScoped<RoomTypeRepository>();
            services.AddScoped<BookingReservationRepository>();
            services.AddScoped<BookingDetailRepository>();
            
            // Register services
            services.AddScoped<AuthenticationService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<RoomService>();
            services.AddScoped<BookingService>();
            services.AddScoped<ReportService>();
            
            return services;
        }
    }
}