using System;
using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.InMemory
{
    public class InMemoryPersonRepository : 
        BaseInMemoryRepository<Person>, IPersonRepository
    {
        private ICityRepository _cityRepository;
        // Constructeur avec pattern d'injection de dépendances (DI)
        public InMemoryPersonRepository(
            ILogger<InMemoryPersonRepository> logger,
            ICityRepository cityRepository) : base(logger)
        {
            _cityRepository = cityRepository;
        }

        public override IQueryable<Person> ModelCollection
        {
            get
            {
                if (_modelCollection == null)
                {
                    _modelCollection = new List<Person>
                    {
                        new Person
                        {
                            Id = 1,
                            FirstName = "Calendau",
                            LastName = "GUQUET",
                            BirthDate = new DateTime(1980,2,28),
                            City = _cityRepository.Single("Toulon")
                        },
                        new Person
                        {
                            Id = 2,
                            FirstName = "John",
                            LastName = "APPLESEED",
                            BirthDate = new DateTime(1971,12,14),
                            City = _cityRepository.Single("Marseille")
                        }
                        ,
                        new Person
                        {
                            Id = 3,
                            FirstName = "Steve",
                            LastName = "JOBS",
                            BirthDate = new DateTime(1949,2,24),
                            City = _cityRepository.Single("Nice")
                        }
                    };
                }
                return _modelCollection.AsQueryable();
            }
        }
    }
}