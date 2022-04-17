using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tranzact.Models.Adapter;
using Tranzact.Models.Request;
using Tranzact.Models.Response;

namespace Tranzact.Services
{
    public class CarrierService
    {
        private string[] Months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public CarrierService() { }
        public IEnumerable<CarrierResponse> GetCarriersAvailable(CarrierRequest carrierRequest, IEnumerable<CarrierFileAdapter> carrierRows)
        {
            IList<CarrierResponse> carriersAvailable = new List<CarrierResponse>();

            carrierRows = carrierRows.Where(x => (x.Plan.Contains(carrierRequest.Plan) || x.Plan == "*") &&
                (x.State == carrierRequest.State || x.State == "*") &&
                (x.MonthOfBirth == Months[carrierRequest.DateOfBirth.Month - 1] || x.MonthOfBirth == "*") &&
                (Int16.Parse(x.MinimumAge) <= carrierRequest.Age && Int16.Parse(x.MaximumAge) >= carrierRequest.Age)
            ).ToList();

            foreach (var carrier in carrierRows)
            {
                carriersAvailable.Add(new CarrierResponse
                {
                    Carrier = carrier.Carrier,
                    Premium = carrier.Premium.ToString()
                });
            }

            return carriersAvailable;
        }
        public bool ValidateAge(CarrierRequest carrierRequest)
        {
            int age = DateTime.Now.Subtract(carrierRequest.DateOfBirth).Days;
            age = age / 365;
            return age == carrierRequest.Age;
        }
    }
}
