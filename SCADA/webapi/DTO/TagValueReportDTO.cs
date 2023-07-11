using webapi.Model;

namespace webapi.DTO
{
    public class TagValueReportDTO
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public string TagType { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string? Value { get; set; }

        public TagValueReportDTO(TagValue tagValue, string tagType, string description)
        {
            Id = tagValue.Id;
            TagId = tagValue.TagBaseId;
            TagType = tagType;
            Description = description;
            Date = tagValue.Date;
            Type = tagValue.Type;
            Value = tagValue.Value;
        }
    }
}
