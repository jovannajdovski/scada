using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using webapi.Enum;

namespace webapi.model
{

    public class DigitalInput
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public IOAdress Adress { get; set; }
        public double ScanTime { get; set; }
        public bool IsScanning { get; set; }


    }
}