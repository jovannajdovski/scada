using webapi.Enum;
using webapi.model;
using webapi.Model;

namespace webapi.DTO
{
    public class AlarmReportDTO
    {
        public int Id { get; set; }
        public AlarmType Type { get; set; }
        public AlarmPriority Priority { get; set; }
        public double Limit { get; set; }
        public DateTime Date { get; set; }
        public int AnalogInputId { get; set; }
        public string AnalogInputDescription { get; set; }
        public double AnalogInputLowLimit { get; set; }
        public double AnalogInputHighLimit { get; set; }
        public string AnalogInputUnit { get; set; }

        public AlarmReportDTO(AlarmTrigger alarmTrigger)
        {
            Id = alarmTrigger.Alarm.Id;
            Type = alarmTrigger.Alarm.Type;
            Priority = alarmTrigger.Alarm.Priority;
            Limit = alarmTrigger.Alarm.Limit;
            Date = alarmTrigger.DateTime;
            AnalogInputId = alarmTrigger.Alarm.AnalogInput.Id;
            AnalogInputDescription = alarmTrigger.Alarm.AnalogInput.Description;
            AnalogInputLowLimit = alarmTrigger.Alarm.AnalogInput.LowLimit;
            AnalogInputHighLimit = alarmTrigger.Alarm.AnalogInput.HighLimit;
            AnalogInputUnit = alarmTrigger.Alarm.AnalogInput.Unit;
        }
    }
}