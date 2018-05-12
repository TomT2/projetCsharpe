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
            string[] cities = city.Split(',');
            string[] departments = department.Split(',');
            string[] categories = category.Split(',');

            var filter = _repository
                .GetAll()
                .Where(i => {
                    bool city_filter = false; 
                    bool department_filter = false;
                    bool category_filter = false;

                    foreach(string c in cities){
                        if(i.Address?.City?.Name.ToLower() == c.ToLower() || c.ToLower() == "all"){
                            city_filter = true;
                        }
                    }

                    foreach(string d in departments){
                        if(i.Address?.City?.Department.Name.ToLower() == d.ToLower() || d.ToLower() == "all"){
                            department_filter = true;
                        }
                    }

                    foreach(string ca in categories){
                        if(i.Category?.Name?.ToLower() == ca.ToLower() || ca.ToLower() == "all"){
                           category_filter = true;
                        }
                    }

                    return city_filter && department_filter && category_filter;   
                })
                .Select(i => i.ToDynamic())
                .ToList();

            return Json(filter);
        }
        
    }
}