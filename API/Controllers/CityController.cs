using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API.Data;
using API.Dto;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        //todo: instance of repository
        private readonly ICityRepository _repo;
        public CityController(ICityRepository repo)
        {
            _repo = repo;
        }
        //todo: get the cities
        [HttpGet]
        public async Task<IEnumerable<City>> GetSortedCitys()
        {
            return await _repo.getCity();
        }

        //todo: delete the chosen city
        [HttpPost("delete")]
        public async Task<ActionResult<bool>> DeleteCity()
        {
            var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            DeleteCityRequest request = JsonConvert.DeserializeObject<DeleteCityRequest>(body);
            return await _repo.DeleteCity(request);
        }

        //todo: add city
        [HttpPost("add")]
        public async Task<ActionResult<bool>> AddCity()
        {
            var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            AddCityRequest request = JsonConvert.DeserializeObject<AddCityRequest>(body);
            return await _repo.AddCity(request);
        }


        //todo: update the city
        [HttpPost("modify")]
        public async Task<ActionResult<bool>> ModifyCity()
        {
            var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            ModifyCityRequest request = JsonConvert.DeserializeObject<ModifyCityRequest>(body);
            return await _repo.ModifyCity(request);
        }

        //todo: get the cities by name
        [HttpGet("{name}")]
        public async Task<IEnumerable<City>> Get(string name)
        {
            GetCityByNameRequest request = new GetCityByNameRequest();
            request.Name = name;
            var result = new [] { await _repo.GetCityByName(request) };
            return result;
        }

    }
}