using System;
using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

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

            var departments = new List<Department>
            {
                new Department { Name = "Alpes-de-Haute-Provence", Number = 4 },
                new Department { Name = "Hautes-Alpes",  Number = 5 },
                new Department { Name = "Alpes-Maritimes", Number = 06 },
                new Department { Name = "Bouches-du-Rhône", Number = 13 },
                new Department { Name = "Var", Number = 83 },
                new Department { Name = "Vaucluse", Number = 84 }
            };
            _departmentRepository.UpdateRange(departments);
            _departmentRepository.Save();

            _logger.LogWarning("Added departments");
        }

        public void AddCities()
        {
            if (_cityRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding cities");

            var cities = new List<City>
            {
                new City 
                { 
                    Name = "Toulon", 
                    Latitude = 1, 
                    Longitude = 1,
                    Department = _departmentRepository.Single("Var")
                },
                new City 
                { 
                    Name = "Marseille", 
                    Latitude = 1, 
                    Longitude = 1,
                    Department = _departmentRepository.Single("Bouches-du-Rhône")
                },
            };
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
                    City = _cityRepository.Single("Toulon"),
                    PostalCode = 83200
                },
                new Address 
                { 
                    Name = "Rue de la Guerre", 
                    Latitude = 1, 
                    Longitude = 1,
                    City = _cityRepository.Single("Marseille"),
                    PostalCode = 13000
                },
            };
            _addressRepository.UpdateRange(addresses);
            _addressRepository.Save();

            _logger.LogWarning("Added addresses");
        }

        public void AddCategories()
        {
            if (_categoryRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding categories");

            var categories = new List<Category>
            {
                new Category 
                { 
                    Name = "Art",
                    Description = "C'est BO"

                },
                new Category 
                { 
                    Name = "Sport",
                    Description = "MUSCU" 
                },
            };
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
                    City = _cityRepository.Single("Marseille")
                }
            };
            _personRepository.UpdateRange(persons);
            _personRepository.Save();

            _logger.LogWarning("Added persons");
        }
    }
}