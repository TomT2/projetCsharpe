using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Address : BaseModel
    { 
        public City City { get;set; }
        public int? CityId { get;set; }
        public int PostalCode { get;set; }
        public float Latitude { get;set; }
        public float Longitude { get;set; }
        public List<InterestPoint> InterestPointCollection { get;set; }
        public int? InterestPointCount => InterestPointCollection?.Count;

        public string BuildAddress(){
            return this.Name + " " + this.PostalCode.ToString() + " " +this.City?.Department?.Name;
        }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.city = City?.ToDynamic();
            response.postalCode = PostalCode;
            response.latitude = Latitude;
            response.longitude = Longitude;
            response.interestPointCount = InterestPointCount;
            response.buildaddress = BuildAddress();
            return response;
        }
    }
}