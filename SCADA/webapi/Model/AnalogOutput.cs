using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using webapi.Model;

namespace webapi.model
{

    public class AnalogOutput:OutputTagBase
    {
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }

    }
}