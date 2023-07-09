using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using webapi.Model;

namespace webapi.model
{

    public class AnalogOutput
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public IOAdress Adress { get; set; }
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
        public virtual List<TagValue> Values { get; set; }

    }
}