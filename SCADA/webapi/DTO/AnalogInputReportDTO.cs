using webapi.model;
using webapi.Model;

namespace webapi.DTO
{
    public class AnalogInputReportDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }

        public AnalogInputReportDTO(AnalogInput analogInput, TagValue lastTagValue)
        {
            Id = analogInput.Id;
            Description = analogInput.Description;
            LowLimit = analogInput.LowLimit;
            HighLimit = analogInput.HighLimit;
            Unit = analogInput.Unit;
            Date = lastTagValue.Date;
            Value = lastTagValue.Value;
        }
    }


}
