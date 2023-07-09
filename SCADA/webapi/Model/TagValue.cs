using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace webapi.Model
{
    public class TagValue
    {
        [Key]
        public int Id { get; set; }
        public int TagId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string? Value { get; set; }
    }
}
