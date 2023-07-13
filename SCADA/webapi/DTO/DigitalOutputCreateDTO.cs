using webapi.Enum;

namespace webapi.DTO
{
    public class DigitalOutputCreateDTO
    {
        public string Description { get; set; }
        //public string ValueType { get; set; }
        public int AddressId { get; set; }
        public DigitalValueType InitialValue { get; set; }
    }
}
