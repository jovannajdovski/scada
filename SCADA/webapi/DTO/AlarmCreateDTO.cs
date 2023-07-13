using webapi.Enum;

namespace webapi.DTO
{
    public class AlarmCreateDTO
    {
        public int Type { get; set; }
        public int Priority { get; set; }
        public double Limit { get; set; }
        public int AnalogInputId { get; set; }
    }
}
