using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Entities;
using API.Dto;
namespace API.Data
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> getCity();
        Task<bool> AddCity(AddCityRequest request);
        Task<bool> ModifyCity(ModifyCityRequest request);
        Task<bool> DeleteCity(DeleteCityRequest request);
        Task<City> GetCityByName(GetCityByNameRequest request);

    }
    public class CityRepository : ICityRepository
    {

        private readonly DataContext _context;
        public CityRepository(DataContext context)
        {
            _context = context;
        }
        
        //todo: method to get cities from database
        public async Task<IEnumerable<City>> getCity()
        {
            return await _context.Cities.ToListAsync();
        }

        //todo: method to add city
        public async Task<bool> AddCity(AddCityRequest request)
        {
            var city= _context.Cities.FirstOrDefault(city  => city.Name == request.Name);
            if (city == null)
            {
                City newCity = new City()
                {
                    Name = request.Name,
                    Temperature = request.Temperature,
                };
                _context.Cities.Add(newCity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //todo: method to modify city
        public async Task<bool> ModifyCity(ModifyCityRequest request)
        {
            var newCity = _context.Cities.FirstOrDefault(city => city.Name == request.NewName);
            var oldCity = _context.Cities.FirstOrDefault(city => city.Name == request.OldName);
            if ((oldCity != null) && (newCity != oldCity))
            {
                return false;
            }
            oldCity.Name = request.NewName;
            oldCity.Temperature = request.Temperature;
            await _context.SaveChangesAsync();
            return true;
        }
        //todo: method to delete city
        public async Task<bool> DeleteCity(DeleteCityRequest request)
        {
            var city = _context.Cities.FirstOrDefault(city => city.Name == request.Name);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<City> GetCityByName(GetCityByNameRequest request)
        {
          return await _context.Cities.FirstOrDefaultAsync(city => city.Name == request.Name);



        }


    }
}