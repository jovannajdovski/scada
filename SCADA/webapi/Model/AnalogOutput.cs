using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace webapi.model
{

    public class AnalogOutput
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        //public int AddressId { get; set; }
        public IOAddress Address { get; set; }
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }

    }
}