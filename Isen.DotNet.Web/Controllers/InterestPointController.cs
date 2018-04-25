using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Isen.DotNet.Web.Models;
using Isen.DotNet.Library.Repositories.Interfaces;
using Isen.DotNet.Library.Models.Implementation;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Web.Controllers
{
    public class InterestPointController : BaseController<InterestPoint>
    {
        public InterestPointController(
            ILogger<InterestPointController> logger,
            IInterestPointRepository repository) 
            : base(logger, repository)
        {
        }
    }
}