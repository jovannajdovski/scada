using webapi.Enum;
using webapi.model;

namespace webapi.DTO
{
    public class AlarmDTO
    {
        public AlarmType Type { get; set; }
        public AlarmPriority Priority { get; set; }
        public double Limit { get; set; }
        public int AnalogInputId { get; set; }

        public AlarmDTO(AlarmType type, AlarmPriority priority, double limit, int analogInputId)
        {
            Type = type;
            Priority = priority;
            Limit = limit;
            AnalogInputId = analogInputId;
        }
    }


}