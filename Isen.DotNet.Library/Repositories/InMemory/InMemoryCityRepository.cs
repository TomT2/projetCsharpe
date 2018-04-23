using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.InMemory
{
    public class InMemoryCityRepository : BaseInMemoryRepository<City>, ICityRepository
    {
        public InMemoryCityRepository(
            ILogger<InMemoryCityRepository> logger) : base(logger)
        {
        }

        public override IQueryable<City> ModelCollection
        {
            get
            {
                if (_modelCollection == null)
                {
                    _modelCollection = new List<City>
                    {
                        new City { Id = 1, Name = "Toulon" },
                        new City { Id = 2, Name = "Marseille" },
                        new City { Id = 3, Name = "Nice" },
                        new City { Id = 4, Name = "Paris" },
                        new City { Id = 5, Name = "Epinal" }
                    };
                }
                return _modelCollection.AsQueryable();
            }
        }

        
    }
}