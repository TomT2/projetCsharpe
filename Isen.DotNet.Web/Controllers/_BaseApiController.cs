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
using Isen.DotNet.Library.Models.Base;
using System.Dynamic;

namespace Isen.DotNet.Web.Controllers
{
    public abstract partial class BaseController<T> : Controller
        where T : BaseModel
    {
        [HttpGet]
        [Route("api/[controller]")]
        public virtual JsonResult GetAll()
        {
            var all = _repository
                .GetAll()
                .Select(t => t.ToDynamic())
                .ToList();
            return Json(all);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public virtual JsonResult GetById(int id)
        {
            var single = _repository.Single(id);
            return Json(single);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public virtual JsonResult Create([FromBody] T model)
        {
            _repository.Update(model);
            _repository.Save();
            return Json(model.ToDynamic());
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public virtual JsonResult Update(int? id, [FromBody] T model)
        {
            if (id == null || model == null || model.Id != id)
                return Json(null);

            _repository.Update(model);
            _repository.Save();
            return Json(model.ToDynamic());
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public virtual IActionResult Remove(int? id)
        {
            if (id == null)
                return BadRequest();
            if (_repository.Single(id.Value) == null)
                return NotFound();
            _repository.Delete(id.Value);
            _repository.Save();
            return new NoContentResult();
        }
    }
}