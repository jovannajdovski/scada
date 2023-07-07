using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using webapi.Enum;

namespace webapi.model
{

    public class Alarm
    {
        [Key]
        public int Id { get; set; }
        public AlarmType Type {get; set;}
        public AlarmPriority Priority { get; set; }
        public double Limit { get; set; }
        public AnalogInput AnalogInput { get; set; }

    }
}