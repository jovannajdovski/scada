using webapi.Enum;
using webapi.model;

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

        public AlarmReportDTO(Alarm alarm)
        {
            Id = alarm.Id;
            Type = alarm.Type;
            Priority = alarm.Priority;
            Limit = alarm.Limit;
            //Date = alarm.Date;
            AnalogInputId = alarm.AnalogInput.Id;
            AnalogInputDescription = alarm.AnalogInput.Description;
            AnalogInputLowLimit = alarm.AnalogInput.LowLimit;
            AnalogInputHighLimit = alarm.AnalogInput.HighLimit;
            AnalogInputUnit = alarm.AnalogInput.Unit;
        }
    }
}