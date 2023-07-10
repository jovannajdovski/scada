using webapi.Enum;

namespace webapi.DTO
{
    public class DigitalInputCreateDTO
    {
        public string Description { get; set; }
        public int AddressId { get; set; }
        public double ScanTime { get; set; }
    }
}
