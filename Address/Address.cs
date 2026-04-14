using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegister
{
    public class Address
    {
        public string Category1 { get; set; }
        public string Floor { get; set; }
        public string BuildingNumber { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public Address(string category1, string floor, string buildingNumber, string street, string postalCode, string state, string country)
        {
            Category1 = category1;            
            Floor = floor;
            BuildingNumber = buildingNumber;
            Street = street;
            PostalCode = postalCode;
            State = state;
            Country = country;
        }

        public override string ToString()
        {
            var parts = new List<string>();
            if (!string.IsNullOrEmpty(Category1)) parts.Add($"{Category1}");
            if (!string.IsNullOrEmpty(Floor)) parts.Add($"Floor {Floor}");
            if (!string.IsNullOrEmpty(BuildingNumber)) parts.Add($"#{BuildingNumber}");
            if (!string.IsNullOrEmpty(Street)) parts.Add(Street);
            if (!string.IsNullOrEmpty(PostalCode) || !string.IsNullOrEmpty(State))
            {
                parts.Add($"{PostalCode} {State}");
            }
            if (!string.IsNullOrEmpty(Country)) parts.Add(Country);

            //return string.Join(", ", parts.Where(p => !string.IsNullOrEmpty(p)));
            return $"{string.Join(", ", parts.Where(p => !string.IsNullOrEmpty(p)))}";
        }

        //public override string ToString()
        //{
        //    return $"Floor: {Floor}, Building Number: {BuildingNumber}, Street: {Street}, Postal Code: {PostalCode}, State: {State}, Country: {Country}";
        //}
    }

}
