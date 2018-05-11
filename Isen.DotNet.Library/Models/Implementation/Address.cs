using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Address : BaseModel
    { 
        public City City { get;set; }
        public int? CityId { get;set; }
        public string PostalCode { get;set; }
        public float Latitude { get;set; }
        public float Longitude { get;set; }

        public InterestPoint InterestPoint {get; set;}
        public int? InterestPointId {get; set;}
        
        public string BuildAddress(){
            return this.Name + " " + this.PostalCode.ToString() + " " +this.City?.Name;
        }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.city = City?.ToDynamic();
            //response.interestpoint = InterestPoint?.ToDynamic();
            response.postalCode = PostalCode;
            response.latitude = Latitude;
            response.longitude = Longitude;
            return response;
        }
    }
}