using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Data;
using Isen.DotNet.Library.Models.Base;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.DbContext
{
    public abstract class BaseDbContextRepository<T> :
        BaseRepository<T>
            where T : BaseModel
    {
        // Référence au contexte EntityFramework (injecté)
        protected readonly ApplicationDbContext Context;

        // Implémentation concrète de la liste de T
        public override IQueryable<T> ModelCollection 
            => Context.Set<T>().AsQueryable();

        public BaseDbContextRepository(
            ILogger<BaseDbContextRepository<T>> logger,
            ApplicationDbContext context) 
            : base(logger)
        {
            Context = context;
        }

        public override void Delete(int id)
        {
            var model = Single(id);
            if (model != null) Context.Remove(model);
        }

        public override void Update(T model)
        {
            if (model.IsNew) Context.Add(model);
            else Context.Update(model);
        }

        public override void Save()
        {
            Context.SaveChanges();
        }
    }
}