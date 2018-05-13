using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Category : BaseModel
    { 
        public string Description { get;set; }
        public List<InterestPoint> InterestPointCollection { get;set; }
        public int? InterestPointCount => InterestPointCollection?.Count;

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.description = Description;
            response.interestPointCount = InterestPointCount;
            return response;
        }
    }
}