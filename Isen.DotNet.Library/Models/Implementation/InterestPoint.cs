using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class InterestPoint : BaseModel
    { 
        public string Description { get;set; }
        public Category Category { get;set; }
        public int? CategoryId { get;set;}
        public Address Address { get;set; }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.description = Description; 
            response.category = Category?.ToDynamic();
            response.address = Address?.ToDynamic();
            return response;
        }
    }
}