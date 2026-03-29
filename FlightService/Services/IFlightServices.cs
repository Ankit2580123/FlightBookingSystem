using FlightService.DTO;
using FlightService.Models;

namespace FlightService.Services
{
    public interface IFlightServices
    {
        Task<Flight> AddFlight(FlightDto dto);
        List<Flight> Search(string source, string destination);
        List<Flight> GetAll();
        Task<Flight> GetById(int id);
        Task<bool> ReduceSeats(int flightId, int seats);
        Task<bool> IncreaseSeats(int flightId, int count);

    }
}
