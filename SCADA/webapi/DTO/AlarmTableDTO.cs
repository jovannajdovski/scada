using webapi.Enum;
using webapi.model;

namespace webapi.DTO
{
    public class AlarmTableDTO
    {
        public int Id { get; set; }
        public AlarmType Type { get; set; }
        public AlarmPriority Priority { get; set; }
        public double Limit { get; set; }
        public string Description { get; set; }

        public AlarmTableDTO(Alarm alarm)
        {
            Id = alarm.Id;
            Type = alarm.Type;
            Priority = alarm.Priority;
            Limit = alarm.Limit;
            Description = alarm.AnalogInput.Description;
        }
    }
}
