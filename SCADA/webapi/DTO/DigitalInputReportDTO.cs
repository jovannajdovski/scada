using webapi.model;
using webapi.Model;

namespace webapi.DTO
{
    public class DigitalInputReportDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }

        public DigitalInputReportDTO(DigitalInput digitalInput, TagValue lastTagValue)
        {
            Id = digitalInput.Id;
            Description = digitalInput.Description;
            Date = lastTagValue.Date;
            Value = lastTagValue.Value;
        }
    }
}
