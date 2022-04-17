using Microsoft.AspNetCore.Mvc;
using System;

namespace Tranzact.Models.Request
{
    [BindProperties]
    public class CarrierRequest
    {
        public DateTime DateOfBirth { get; set; }
        public string State { get; set; }
        public int Age { get; set; }
        public char Plan { get; set; }
    }
}
