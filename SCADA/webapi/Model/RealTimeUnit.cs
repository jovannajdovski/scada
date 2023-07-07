using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using webapi.model;

namespace webapi.Enum
{
    public class DigitalOutput
    {
        [Key]
        public int Id { get; set; }
        public double HighLimit { get; set; }
        public double LowLimit { get; set; }
        public IOAdress Adress { get; set; }
    }
}