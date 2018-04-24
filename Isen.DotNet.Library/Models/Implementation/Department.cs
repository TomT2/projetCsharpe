using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Departement : BaseModel
    { 
        public int Number { get;set; }
        public List<City> CityCollection { get;set; }
        public int? CityCount => CityCollection?.Count;

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.cityCount = CityCount;
            response.number = Number;
            return response;
        }
    }
}