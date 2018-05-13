using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class City : BaseModel
    { 
        public float? Latitude { get;set; }
        public float? Longitude { get;set; }
        public List<Address> AddressCollection { get;set; }
        public int? AddressCount => AddressCollection?.Count;
        public Department Department { get;set; }
        public int? DepartmentId { get;set; }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.latitude = Latitude;
            response.longitude = Longitude;
            response.addressCount = AddressCount;
            response.department = Department?.ToDynamic();
            return response;
        }
    }
}