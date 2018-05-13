using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Isen.DotNet.Library.Data
{
    public class SeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SeedData> _logger;
        private readonly ICityRepository _cityRepository;
        private readonly IInterestPointRepository _interestPointRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDepartmentRepository _departmentRepository;




        public SeedData(
            ApplicationDbContext context,
            ILogger<SeedData> logger,
            ICityRepository cityRepository,
            IInterestPointRepository interestPointRepository,
            ICategoryRepository categoryRepository,
            IDepartmentRepository departmentRepository)
        {
            _context = context;
            _logger = logger;
            _cityRepository = cityRepository;
            _interestPointRepository = interestPointRepository;
            _categoryRepository = categoryRepository;
            _departmentRepository = departmentRepository;
        }

        public void DropDatabase()
        {
            var deleted = _context.Database.EnsureDeleted();
            var not = deleted ? "" : "not ";
            _logger.LogWarning($"Database was {not}dropped.");
        }

        public void CreateDatabase()
        {
            var created = _context.Database.EnsureCreated();
            var not = created ? "" : "not ";
            _logger.LogWarning($"Database was {not}created.");
        }

        public void AddDepartments()
        {
            if (_departmentRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding departments");

            var departments = new List<Department>();

            string departments_json = File.ReadAllText("../Isen.DotNet.Library/data/json/departments.json");
            departments = JsonConvert.DeserializeObject<List<Department>>(departments_json);

            _departmentRepository.UpdateRange(departments);
            _departmentRepository.Save();

            _logger.LogWarning("Added departments");
        }

        public void AddCities()
        {
            if (_cityRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding cities");

            var cities = new List<City>();

            string var_cities_json = File.ReadAllText("../Isen.DotNet.Library/data/json/communes/var.json");
            List<City> cities_var = JsonConvert.DeserializeObject<List<City>>(var_cities_json);
            foreach(City city in cities_var)
            {
                city.Department = _departmentRepository.Single("Var");
            }

            string alpes_provence_cities_json = File.ReadAllText("../Isen.DotNet.Library/data/json/communes/alpes-de-haute-provence.json");
            List<City> cities_ap = JsonConvert.DeserializeObject<List<City>>(alpes_provence_cities_json);
            foreach(City city in cities_ap)
            {
                city.Department = _departmentRepository.Single("Alpes-de-Haute-Provence");
            }

            string alpes_maritimes_cities_json = File.ReadAllText("../Isen.DotNet.Library/data/json/communes/alpes-maritimes.json");
            List<City> cities_am = JsonConvert.DeserializeObject<List<City>>(alpes_maritimes_cities_json);
            foreach(City city in cities_am)
            {
                city.Department = _departmentRepository.Single("Alpes-Maritimes");
            }

            string bouches_du_rhone_cities_json = File.ReadAllText("../Isen.DotNet.Library/data/json/communes/bouches-du-rhone.json");
            List<City> cities_b = JsonConvert.DeserializeObject<List<City>>(bouches_du_rhone_cities_json);
            foreach(City city in cities_b)
            {
                city.Department = _departmentRepository.Single("Bouches-du-Rhône");
            }

            string hautes_alpes_cities_json = File.ReadAllText("../Isen.DotNet.Library/data/json/communes/hautes-alpes.json");
            List<City> cities_h = JsonConvert.DeserializeObject<List<City>>(hautes_alpes_cities_json);
            foreach(City city in cities_h)
            {
                city.Department = _departmentRepository.Single("Hautes-Alpes");
            }

            string vaucluse_cities_json = File.ReadAllText("../Isen.DotNet.Library/data/json/communes/vaucluse.json");
            List<City> cities_vaucluse = JsonConvert.DeserializeObject<List<City>>(vaucluse_cities_json);
            foreach(City city in cities_vaucluse)
            {
                city.Department = _departmentRepository.Single("Vaucluse");
            }

            cities.AddRange(cities_var);
            cities.AddRange(cities_ap);
            cities.AddRange(cities_am);
            cities.AddRange(cities_b);
            cities.AddRange(cities_h);
            cities.AddRange(cities_vaucluse);
            
            _cityRepository.UpdateRange(cities);
            _cityRepository.Save();

            _logger.LogWarning("Added cities");
        }

        public void AddCategories()
        {
            if (_categoryRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding categories");         

            var categories = new List<Category>();
            string categories_json = File.ReadAllText("../Isen.DotNet.Library/data/json/categories.json");
            categories = JsonConvert.DeserializeObject<List<Category>>(categories_json);

            _categoryRepository.UpdateRange(categories);
            _categoryRepository.Save();

            _logger.LogWarning("Added categories");
        }

        public void AddInterestPoints()
        {
            if (_interestPointRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding interestPoints");

            var interestPoints = new List<InterestPoint>();
            string poi_json = File.ReadAllText("../Isen.DotNet.Library/data/json/points-of-interest.json");
            interestPoints = JsonConvert.DeserializeObject<List<InterestPoint>>(poi_json);

            interestPoints[0].Address.City = _cityRepository.Single("Saint-Tropez");
            interestPoints[0].Category = _categoryRepository.Single("Restaurant");
            
            interestPoints[1].Address.City = _cityRepository.Single("Marseille");
            interestPoints[1].Category = _categoryRepository.Single("Parc");

            interestPoints[2].Address.City = _cityRepository.Single("Le Castellet");
            interestPoints[2].Category = _categoryRepository.Single("Loisir");

            interestPoints[3].Address.City = _cityRepository.Single("La Barben");
            interestPoints[3].Category = _categoryRepository.Single("Parc");

            interestPoints[4].Address.City = _cityRepository.Single("Le Pradet");
            interestPoints[4].Category = _categoryRepository.Single("Loisir");

            interestPoints[5].Address.City = _cityRepository.Single("Antibes");
            interestPoints[5].Category = _categoryRepository.Single("Loisir");

            interestPoints[6].Address.City = _cityRepository.Single("Nice");
            interestPoints[6].Category = _categoryRepository.Single("Loisir");

            interestPoints[7].Address.City = _cityRepository.Single("Roussillon");
            interestPoints[7].Category = _categoryRepository.Single("Parc");

            interestPoints[8].Address.City = _cityRepository.Single("Sisteron");
            interestPoints[8].Category = _categoryRepository.Single("Loisir");

            interestPoints[9].Address.City = _cityRepository.Single("La Valette-du-Var");
            interestPoints[9].Category = _categoryRepository.Single("Shopping");

            interestPoints[10].Address.City = _cityRepository.Single("Marseille");
            interestPoints[10].Category = _categoryRepository.Single("Restaurant");

            interestPoints[11].Address.City = _cityRepository.Single("Toulon");
            interestPoints[11].Category = _categoryRepository.Single("Restaurant");

            interestPoints[12].Address.City = _cityRepository.Single("Toulon");
            interestPoints[12].Category = _categoryRepository.Single("Restaurant");

            interestPoints[13].Address.City = _cityRepository.Single("Briancon");
            interestPoints[13].Category = _categoryRepository.Single("Hébergement");

            interestPoints[14].Address.City = _cityRepository.Single("Moustiers-Sainte-Marie");
            interestPoints[14].Category = _categoryRepository.Single("Hébergement");

            interestPoints[15].Address.City = _cityRepository.Single("Marseille");
            interestPoints[15].Category = _categoryRepository.Single("Shopping");

            interestPoints[16].Address.City = _cityRepository.Single("Avignon");
            interestPoints[16].Category = _categoryRepository.Single("Hébergement");

            interestPoints[17].Address.City = _cityRepository.Single("Grimaud");
            interestPoints[17].Category = _categoryRepository.Single("Hébergement");

            interestPoints[18].Address.City = _cityRepository.Single("Hyeres");
            interestPoints[18].Category = _categoryRepository.Single("Parc");

            interestPoints[19].Address.City = _cityRepository.Single("Le Pontet");
            interestPoints[19].Category = _categoryRepository.Single("Shopping");

            interestPoints[20].Address.City = _cityRepository.Single("Marseille");
            interestPoints[20].Category = _categoryRepository.Single("Shopping");

            _interestPointRepository.UpdateRange(interestPoints);
            _interestPointRepository.Save();

            _logger.LogWarning("Added interestPoints");
        }

    }
}