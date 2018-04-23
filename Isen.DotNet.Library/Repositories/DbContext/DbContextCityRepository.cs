using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Data;
using Isen.DotNet.Library.Models.Base;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.DbContext
{
    public class DbContextCityRepository :
        BaseDbContextRepository<City>, ICityRepository
    {
        public DbContextCityRepository(
            ILogger<DbContextCityRepository> logger, 
            ApplicationDbContext context) 
            : base(logger, context)
        {
        }

        public override IQueryable<City> Includes(
            IQueryable<City> queryable)
                => queryable.Include(c => c.PersonCollection);
    }
}