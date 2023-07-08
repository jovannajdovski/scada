using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using webapi.Enum;

namespace webapi.model
{

    public class DigitalOutput
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public IOAddress Address { get; set; }
        public DigitalValueType InitialValue { get; set; }
    }
}