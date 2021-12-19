using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_API.Models
{
    public class Brewery
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "name")]
        public string Name { get; set; }

        [JsonProperty(propertyName: "brewery_type")]
        public string BreweryType { get; set; }

        [JsonProperty(propertyName: "street")]
        public string Street { get; set; }

        [JsonProperty(propertyName: "address_2")]
        public string Address2 { get; set; }

        [JsonProperty(propertyName: "address_3")]
        public string Address3 { get; set; }

        [JsonProperty(propertyName: "city")]
        public string City { get; set; }

        [JsonProperty(propertyName: "state")]
        public string State { get; set; }

        [JsonProperty(propertyName: "county_province")]
        public string CountyProvince { get; set; }

        [JsonProperty(propertyName: "postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty(propertyName: "country")]
        public string Country { get; set; }

        [JsonProperty(propertyName: "phone")]
        public string Phone { get; set; }

        [JsonProperty(propertyName: "website_url")]
        public string WebsiteUrl { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
