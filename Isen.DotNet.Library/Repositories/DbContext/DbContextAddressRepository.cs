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
    public class DbContextAddressRepository :
        BaseDbContextRepository<Address>, IAddressRepository
    {
        public DbContextAddressRepository(
            ILogger<DbContextAddressRepository> logger, 
            ApplicationDbContext context) 
            : base(logger, context)
        {
        }

        public override IQueryable<Address> Includes(
            IQueryable<Address> queryable)
                => queryable
                .Include(a => a.City)
                .Include(a => a.InterestPoint);
    }
}