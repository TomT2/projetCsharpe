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

        [HttpGet]
        [Route("api/[controller]/Category/{category?}")]
        public virtual JsonResult GetByCategory(string category)
        {
            var allByCategory = _repository
                .GetAll()
                .Where(i => i.Category?.Name?.ToLower() == category.ToLower())
                .Select(i => i.ToDynamic())
                .ToList();
            return Json(allByCategory);
        }

        [HttpGet]
        [Route("api/[controller]/City/{city?}")]
        public virtual JsonResult GetByCity(string city)
        {
            var allByCity = _repository
                .GetAll()
                .Where(i => i.Address?.City?.Name.ToLower() == city.ToLower())
                .Select(i => i.ToDynamic())
                .ToList();
            return Json(allByCity);
        }

        [HttpGet]
        [Route("api/[controller]/Department/{department?}")]
        public virtual JsonResult GetByDepartment(string department)
        {
            var allByDepartment = _repository
                .GetAll()
                .Where(i => i.Address?.City?.Department.Name.ToLower() == department.ToLower())
                .Select(i => i.ToDynamic())
                .ToList();
            return Json(allByDepartment);
        }

        [HttpGet]
        [Route("api/[controller]/City/{city?}/Department/{department?}/Category/{category?}")]
        public virtual JsonResult Get(string city="all", string department="all", string category="all")
        {
            var filter = _repository
                .GetAll()
                .Where(i => (i.Address?.City?.Name.ToLower() == city.ToLower() || city.ToLower() == "all")&&
                            (i.Address?.City?.Department.Name.ToLower() == department.ToLower() || department.ToLower() == "all")&&
                            (i.Category?.Name?.ToLower() == category.ToLower() || category.ToLower() == "all"))
                .Select(i => i.ToDynamic())
                .ToList();
            return Json(filter);
        }
        
    }
}