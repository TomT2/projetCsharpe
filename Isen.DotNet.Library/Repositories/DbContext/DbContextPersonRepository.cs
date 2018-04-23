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
    public class DbContextPersonRepository :
        BaseDbContextRepository<Person>, IPersonRepository
    {
        public DbContextPersonRepository(
            ILogger<DbContextPersonRepository> logger, 
            ApplicationDbContext context) 
            : base(logger, context)
        {
        }

        public override IQueryable<Person> Includes(
            IQueryable<Person> queryable)
                => queryable.Include(p => p.City);
    }
}