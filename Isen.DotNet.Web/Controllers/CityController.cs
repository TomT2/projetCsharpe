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
    public class CityController : BaseController<City>
    {
        public CityController(
            ILogger<CityController> logger,
            ICityRepository repository) 
            : base(logger, repository)
        {
        }

        [HttpGet]
        [Route("api/[controller]/Department/{Department?}")]
        public virtual JsonResult GetByDepartment(string department)
        {
            var allByDepartment = _repository
                .GetAll()
                .Where(c => c.Department.Name.ToLower() == department.ToLower())
                .Select(c => c.ToDynamic())
                .ToList();
            return Json(allByDepartment);
        }
    }
}