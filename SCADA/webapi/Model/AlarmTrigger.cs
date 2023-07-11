using System.ComponentModel.DataAnnotations;
using webapi.model;

namespace webapi.Model
{
    public class AlarmTrigger
    {
        [Key]
        public int Id { get; set; }

        public Alarm Alarm { get; set; }
        public DateTime DateTime { get; set; }
    }
}