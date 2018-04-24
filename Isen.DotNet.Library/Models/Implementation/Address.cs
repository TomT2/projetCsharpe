using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Address : BaseModel
    { 
        public string Text { get;set; }
        public City City { get;set; }
        public int? CityId { get;set; }
        public int PostalCode { get;set; }
        public float Latitude { get;set; }
        public float Longitude { get;set; }
        public InterestPoint InterestPoint { get;set; }
        public int? InterestPointId { get;set; }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.text = Text; 
            response.city = City?.ToDynamic();
            response.postalCode = PostalCode;
            response.latitude = Latitude;
            response.longitude = Longitude;
            response.InterestPoint = InterestPoint?.ToDynamic();
            return response;
        }
    }
}