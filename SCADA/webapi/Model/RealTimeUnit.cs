using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using webapi.model;

namespace webapi.model
{
    public class RealTimeUnit
    {
        [Key]
        public int Id { get; set; }
        public double HighLimit { get; set; }
        public double LowLimit { get; set; }
        public int AddressId { get; set; }
        public IOAddress Address { get; set; }
        public RealTimeUnit()
        {
        }

        public void SetProperties(double highLimit, double lowLimit, IOAddress address)
        {
            HighLimit = highLimit;
            LowLimit = lowLimit;
            Address = address;
            Console.WriteLine("setovan" + address.Id);
            AddressId = address.Id;
        }
    }
}