using webapi.Enum;
using webapi.model;
using webapi.Model;

namespace webapi.DTO
{
    public class AllAlarmsDTO
    {
        public int Id { get; set; }

        public AlarmType Type { get; set; }
        public AlarmPriority Priority { get; set; }
        public double Limit { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public AllAlarmsDTO(AlarmTrigger trigger)
        {
            this.Priority = trigger.Alarm.Priority;
            this.Id = trigger.Id;
            this.Type = trigger.Alarm.Type;
            this.Limit = trigger.Alarm.Limit;
            this.Description = trigger.Alarm.AnalogInput.Description;
            this.DateTime = trigger.DateTime;
        }
    }
}