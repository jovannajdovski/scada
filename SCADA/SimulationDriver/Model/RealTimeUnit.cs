using SimulationDriver.model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;

namespace SimulationDriver.model
{
    public class RealTimeUnit
    {
        [Key]
        public int Id { get; set; }
        public double HighLimit { get; set; }
        public double LowLimit { get; set; }
        public int AddressId { get; set; }
        public IOAddress Address { get; set; }
        
    }
}