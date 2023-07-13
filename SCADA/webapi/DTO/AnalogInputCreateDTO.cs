using webapi.model;

namespace webapi.DTO
{
    public class AnalogInputCreateDTO
    {
        public string Description { get; set; }
        //public string ValueType { get; set; }
        public int AddressId { get; set; }
        public double ScanTime { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
    }
}
