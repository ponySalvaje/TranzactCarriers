using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Tranzact.Models.Adapter;
using Tranzact.Models.Request;
using Tranzact.Models.Response;
using Tranzact.AppServices;
using Tranzact.Utils;
using Tranzact.Services;

namespace Tranzact.Providers
{
    public class CarrierProvider
    {
        private string FILE_DIRECTORY { get; }
        private CarrierService _carrierService { get; }
        public CarrierProvider(IOptions<MyAppSettings> _options)
        {
            FILE_DIRECTORY = _options.Value.FileDirectory;
            _carrierService = new CarrierService();
        }
        public IEnumerable<CarrierResponse> GetCarriers(CarrierRequest carrierRequest)
        {
            IEnumerable<CarrierResponse> carriersAvailable = new List<CarrierResponse>();

            // validate the data
            bool isValid = _carrierService.ValidateAge(carrierRequest);

            if (isValid)
            {
                // get the information from the Excel File
                IEnumerable<CarrierFileAdapter> carrierRows = Utils.Utils.GetCarriersFromFile(FILE_DIRECTORY);

                // match the criteria from the request to the rows in information
                carriersAvailable = _carrierService.GetCarriersAvailable(carrierRequest, carrierRows);
            }
            else
            {
                throw new Exception();
            }

            return carriersAvailable;
        }
    }
}
