using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using webapi.Enum;
using webapi.Model;

namespace webapi.model
{

    public class DigitalOutput
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public IOAdress Adress { get; set; }
        public DigitalValueType InitialValue { get; set; }

        public virtual List<TagValue> Values { get; set; }
    }
}