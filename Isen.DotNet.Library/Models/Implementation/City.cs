using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class City : BaseModel
    { 
        public List<Person> PersonCollection { get;set; }
        public int? PersonCount => PersonCollection?.Count;
        public float Latitude { get;set; }
        public float Longitude { get;set; }
        public List<Address> AddressCollection { get;set; }
        public int? AddressCount => AddressCollection?.Count;
        public Departement Departement { get;set; }
        public int? DepartementId { get;set; }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.nb = PersonCount;
            response.latitude = Latitude;
            response.longitude = Longitude;
            response.addressCount = AddressCount;
            response.department = Departement?.ToDynamic();
            return response;
        }
    }
}