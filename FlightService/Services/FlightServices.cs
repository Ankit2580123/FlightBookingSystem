using FlightService.DTO;
using FlightService.Models;

namespace FlightService.Services
{
    public class FlightServices:IFlightServices
    {
        private readonly FlightDbContext _context;

        public FlightServices(FlightDbContext context)
        {
            _context = context;
        }
        public async Task<Flight> AddFlight(FlightDto dto)
        {
            var flight = new Flight
            {
                FlightNumber = dto.FlightNumber,
                Source = dto.Source,  
                Destination = dto.Destination,
                DepartureTime = dto.DepartureTime,
                TotalSeats = dto.TotalSeats,
                AvailableSeats = dto.TotalSeats,
                Price = dto.Price
            };

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return flight;
        }

        public List<Flight> Search(string source, string destination)
        {
            return _context.Flights
                .Where(f => f.Source == source && f.Destination == destination)
                .ToList();
        }

        public List<Flight> GetAll()
        {
            return _context.Flights.ToList();
        }

        public async Task<bool> ReduceSeats(int flightId, int seats)
        {
            var flight = await _context.Flights.FindAsync(flightId);

            if (flight == null || flight.AvailableSeats < seats)
            {
                return false;
            }
            flight.AvailableSeats -= seats;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Flight> GetById(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<bool> IncreaseSeats(int flightId, int count)
        {
            var flight = await _context.Flights.FindAsync(flightId);

            if (flight == null)
            {
                return false;
            }
            flight.AvailableSeats += count;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
