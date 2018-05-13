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
    public class DbContextInterestPointRepository :
        BaseDbContextRepository<InterestPoint>, IInterestPointRepository
    {
        public DbContextInterestPointRepository(
            ILogger<DbContextInterestPointRepository> logger, 
            ApplicationDbContext context) 
            : base(logger, context)
        {
        }

        public override IQueryable<InterestPoint> Includes(
            IQueryable<InterestPoint> queryable)
                => queryable
                .Include(i => i.Category)
                .Include(i => i.Address).ThenInclude(a => a.City).ThenInclude(c => c.Department);
    }
}