using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using webapi.Enum;
using webapi.Model;

namespace webapi.model
{

    public class AnalogInput : InputTagBase
    {
        public virtual List<Alarm> Alarms { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
    }
}