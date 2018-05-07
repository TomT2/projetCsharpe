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
        private readonly IPersonRepository _personRepository;
        private readonly IInterestPointRepository _interestPointRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDepartmentRepository _departmentRepository;




        public SeedData(
            ApplicationDbContext context,
            ILogger<SeedData> logger,
            ICityRepository cityRepository,
            IPersonRepository personRepository,
            IInterestPointRepository interestPointRepository,
            IAddressRepository addressRepository,
            ICategoryRepository categoryRepository,
            IDepartmentRepository departmentRepository)
        {
            _context = context;
            _logger = logger;
            _cityRepository = cityRepository;
            _personRepository = personRepository;
            _interestPointRepository = interestPointRepository;
            _addressRepository = addressRepository;
            _categoryRepository = categoryRepository;
            _departmentRepository = departmentRepository;
        }

        public void LoadJsonFile(){
            string varjson = File.ReadAllText("../Isen.DotNet.Library/data/json/communes/test.json");
            //List<City> items = JsonConvert.DeserializeObject<List<Item>>(varjson);;
            List<City> items = JsonConvert.DeserializeObject<List<City>>(varjson);
            foreach(City city in items){
                Console.WriteLine(city.Name);
                Console.WriteLine(city.Id);
                Console.WriteLine(city.Latitude);
                Console.WriteLine(city.Longitude);
            }
        
            _cityRepository.UpdateRange(items);
            _cityRepository.Save();
 
            _logger.LogWarning("Added cities");    
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
            List<City> cities_am = JsonConvert.DeserializeObject<List<City>>(alpes_provence_cities_json);
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
            cities.AddRange(cities_am);
            cities.AddRange(cities_b);
            cities.AddRange(cities_h);
            cities.AddRange(cities_vaucluse);
            
            _cityRepository.UpdateRange(cities);
            _cityRepository.Save();

            _logger.LogWarning("Added cities");
        }

        public void AddAddresses()
        {
            if (_addressRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding addresses");

            var addresses = new List<Address>
            {
                new Address 
                { 
                    Name = "Rue de la Paix", 
                    Latitude = 1.32F, 
                    Longitude = 1.234F,
                    City = _cityRepository.Single("Carqueiranne"),
                    PostalCode = 83200
                }
            };
            _addressRepository.UpdateRange(addresses);
            _addressRepository.Save();

            _logger.LogWarning("Added addresses");
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

            var interestPoints = new List<InterestPoint>
            {
                new InterestPoint 
                { 
                    Name = "Tour eiffel",
                    Description = "C'est haut",
                    Category = _categoryRepository.Single("Art"),
                    Address = _addressRepository.Single("Rue de la Paix")
                },
                new InterestPoint 
                { 
                    Name = "Colisée",
                    Description = "C'est barbare",
                    Category = _categoryRepository.Single("Sport"),
                    Address = _addressRepository.Single("Rue de la Guerre")
                },
            };
            _interestPointRepository.UpdateRange(interestPoints);
            _interestPointRepository.Save();

            _logger.LogWarning("Added interestPoints");
        }

        public void AddPersons()
        {
            if (_personRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding persons");

            var persons = new List<Person>
            {
                new Person
                {
                    FirstName = "Calendau",
                    LastName = "GUQUET",
                    BirthDate = new DateTime(1980,2,28),
                    City = _cityRepository.Single("Toulon")
                },
                new Person
                {
                    FirstName = "John",
                    LastName = "APPLESEED",
                    BirthDate = new DateTime(1971,12,14),
                    City = _cityRepository.Single("Marseille")
                },
                new Person
                {
                    FirstName = "Steve",
                    LastName = "JOBS",
                    BirthDate = new DateTime(1949,2,24),
                    City = _cityRepository.Single("La Garde")
                }
            };
            _personRepository.UpdateRange(persons);
            _personRepository.Save();

            _logger.LogWarning("Added persons");
        }
    }
}