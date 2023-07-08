using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SimulationDriver.model
{

    public class IOAdress
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public IOAdress(int id, string type, string value) 
        {
            Id = id;
            Type = type;
            Value = value;
        }
    }
}