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
    public class DbContextCategoryRepository :
        BaseDbContextRepository<Category>, ICategoryRepository
    {
        public DbContextCategoryRepository(
            ILogger<DbContextCategoryRepository> logger, 
            ApplicationDbContext context) 
            : base(logger, context)
        {
        }

        public override IQueryable<Category> Includes(
            IQueryable<Category> queryable)
                => queryable
                .Include(c => c.InterestPointCollection);
    }
}